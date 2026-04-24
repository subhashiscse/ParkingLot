using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

public class AccountController : Controller
{
    private readonly ICustomerService _customerService;
    private readonly IVehicleService _vehicleService;

    public AccountController(ICustomerService customerService, IVehicleService vehicleService)
    {
        _customerService = customerService;
        _vehicleService = vehicleService;
    }
    
    // REGISTER
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var existing = _customerService.GetByEmail(model.Email);

        if (existing != null)
        {
            ModelState.AddModelError("Email", "Email already exists");
            return View(model);
        }

        var customer = new Customer
        {
            Name = model.Name,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Username = model.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
            BillingAddressLine = model.Address,
            IsActive = true
        };

        _customerService.Create(customer);

        return RedirectToAction("Login");
    }

    // LOGIN
    
    [HttpGet]
    public IActionResult Login()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId != null)
        {
            return RedirectToAction("Profile");
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = _customerService.GetByEmail(model.Email);

        if (user == null)
        {
            ModelState.AddModelError("Email", "Email not found");
            return View(model);
        }

        if (!BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
        {
            ModelState.AddModelError("Password", "Incorrect password");
            return View(model);
        }

        HttpContext.Session.SetInt32("UserId", user.CustomerId);
        HttpContext.Session.SetString("UserName", user.Name);
        HttpContext.Session.SetString("UserEmail", user.Email);
        HttpContext.Session.SetString("UserRole", user.CustomerType);

        return RedirectToAction("Profile");
    }

    // LOGOUT
    
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    // PROFILE
    
    [HttpGet]
    public IActionResult Profile()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login");

        var user = _customerService.GetById(userId.Value);

        var model = new ProfileViewModel
        {
            Name = user.Name,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Address = user.BillingAddressLine
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Profile(ProfileViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login");

        var user = _customerService.GetById(userId.Value);

        user.Name = model.Name;
        user.PhoneNumber = model.PhoneNumber;
        user.BillingAddressLine = model.Address;

        _customerService.Update(user);

        return RedirectToAction("Profile");
    }

    // VEHICLES
    
    public IActionResult Vehicles()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login");

        var vehicles = _vehicleService.GetByCustomerId(userId.Value);
        return View(vehicles);
    }

    // ADD VEHICLE (GET)
    [HttpGet]
    public IActionResult AddVehicle()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return RedirectToAction("Login", "Account");

        return View(new VehicleViewModel()); // 🔥 FIX HERE
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddVehicle(VehicleViewModel model)
    {
        if (model == null)
        {
            ModelState.AddModelError("", "Form data not received");
            return View(new VehicleViewModel());
        }

        if (!ModelState.IsValid)
            return View(model);

        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var vehicle = new Vehicle
        {
            RegistrationNumber = model.RegistrationNumber,
            Make = model.Make,
            VehicleModel = model.VehicleModel,
            Colour = model.Colour,
            CustomerId = userId.Value,
            CustomerName =  HttpContext.Session.GetString("UserName")
        };

        _vehicleService.Add(vehicle);
        return RedirectToAction("Vehicles");
    }

    public IActionResult DeleteVehicle(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login");

        var vehicle = _vehicleService.GetById(id);

        if (vehicle != null && vehicle.CustomerId == userId.Value)
        {
            _vehicleService.Delete(id);
        }

        return RedirectToAction("Vehicles");
    }
    [HttpGet]
    public IActionResult EditVehicle(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login");

        var vehicle = _vehicleService.GetById(id);

        if (vehicle == null || vehicle.CustomerId != userId)
            return NotFound();

        var model = new VehicleViewModel
        {
            VehicleId = vehicle.VehicleId,
            RegistrationNumber = vehicle.RegistrationNumber,
            Make = vehicle.Make,
            VehicleModel = vehicle.VehicleModel,
            Colour = vehicle.Colour
        };

        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditVehicle(VehicleViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login");

        var vehicle = _vehicleService.GetById(model.VehicleId);

        if (vehicle == null || vehicle.CustomerId != userId)
            return NotFound();

        vehicle.RegistrationNumber = model.RegistrationNumber;
        vehicle.Make = model.Make;
        vehicle.VehicleModel = model.VehicleModel;
        vehicle.Colour = model.Colour;

        _vehicleService.Update(vehicle);

        return RedirectToAction("Vehicles");
    }
    [HttpGet]
    public IActionResult EditProfile()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return RedirectToAction("Login");

        var user = _customerService.GetById(userId.Value);

        if (user == null)
            return NotFound();

        var model = new ProfileViewModel
        {
            Name = user.Name,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Address = user.BillingAddressLine
        };

        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditProfile(ProfileViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return RedirectToAction("Login");

        var user = _customerService.GetById(userId.Value);

        if (user == null)
            return NotFound();

        user.Name = model.Name;
        user.PhoneNumber = model.PhoneNumber;
        user.BillingAddressLine = model.Address;

        _customerService.Update(user);

        return RedirectToAction("Profile");
    }
}