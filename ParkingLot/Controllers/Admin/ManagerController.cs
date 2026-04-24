using Microsoft.AspNetCore.Mvc;

public class ManagerController : Controller
{
    private readonly ICustomerService _customerService;
    private readonly IReservationService _reservationService;

    public ManagerController(ICustomerService customerService,
        IReservationService reservationService)
    {
        _customerService = customerService;
        _reservationService = reservationService;
    }
    public IActionResult Customer()
    {
        var role = HttpContext.Session.GetString("UserRole");

        if (role != "Admin")
            return RedirectToAction("Login", "Account");

        var customers = _customerService.GetAll();

        return View(customers); 
    }
    [HttpGet]
    public IActionResult CreateCustomer()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateCustomer(Customer model)
    {
        if (!ModelState.IsValid)
            return View(model);

        model.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.PasswordHash);
        model.IsActive = true;

        _customerService.Create(model);

        return RedirectToAction("Customer");
    }
    [HttpGet]
    public IActionResult EditCustomer(int id)
    {
        var customer = _customerService.GetById(id);

        if (customer == null)
            return NotFound();

        return View(customer);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditCustomer(Customer model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var existing = _customerService.GetById(model.CustomerId);

        if (existing == null)
            return NotFound();

        existing.Name = model.Name;
        existing.Email = model.Email;
        existing.PhoneNumber = model.PhoneNumber;
        existing.CustomerType = model.CustomerType;
        existing.BillingAddressLine = model.BillingAddressLine;
        existing.City = model.City;
        existing.Postcode = model.Postcode;

        _customerService.Update(existing);

        return RedirectToAction("Customer");
    }
    public IActionResult DeleteCustomer(int id)
    {
        var customer = _customerService.GetById(id);

        if (customer != null)
        {
            _customerService.Delete(customer.CustomerId);
        }

        return RedirectToAction("Customer");
    }

    // =========================
    // CUSTOMER DETAILS
    // =========================
    public IActionResult CustomerDetails(int id)
    {
        var customer = _customerService.GetById(id);

        if (customer == null)
            return NotFound();

        return View(customer);
    }
    public IActionResult UpdateReservationStatus(int id, string status)
    {
        
        var reservation = _reservationService.GetById(id);

        if (reservation == null)
            return NotFound();
        reservation.Status = status;
        _reservationService.Update(reservation);
        var reservations = _reservationService.GetAll();
        
        return View("ReportsBookings",reservations);
    }
    
}