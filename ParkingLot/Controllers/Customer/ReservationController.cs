using Microsoft.AspNetCore.Mvc;

public class ReservationController : Controller
{
    private readonly IGarageService _garageService;
    private readonly IVehicleService _vehicleService;
    private readonly IReservationService _reservationService;

    public ReservationController(
        IGarageService garageService,
        IVehicleService vehicleService,
        IReservationService reservationService)
    {
        _garageService = garageService;
        _vehicleService = vehicleService;
        _reservationService = reservationService;
    }

    // =========================
    // STEP 1: BOOK PAGE (SELECT VEHICLE)
    // =========================
    public IActionResult Book(int garageId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return RedirectToAction("Login", "Account");

        var vehicles = _vehicleService.GetByCustomerId(userId.Value);

        var model = new BookingViewModel
        {
            GarageId = garageId,
            Vehicles = vehicles
        };

        return View(model);
    }

    // =========================
    // STEP 2: CONFIRM BOOKING
    // =========================
    [HttpPost]
    public IActionResult ConfirmBooking(BookingViewModel model)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return RedirectToAction("Login", "Account");

        if (!ModelState.IsValid)
        {
            model.Vehicles = _vehicleService.GetByCustomerId(userId.Value);
            return View("Book", model);
        }

        var cost = CalculateCost(model.GarageId, model.StartDateTime, model.EndDateTime);
        var vehicle = _vehicleService.GetById(model.VehicleId);
        var garage = _garageService.GetById(model.GarageId);
        var reservation = new Reservation
        {
            CustomerId = userId.Value,
            CustomerName = HttpContext.Session.GetString("UserName"),
            GarageId = model.GarageId,
            GarageName =  garage.Name,
            VehicleId = model.VehicleId,
            VehicleName = vehicle.RegistrationNumber,
            StartDateTime = model.StartDateTime,
            EndDateTime = model.EndDateTime,
            Cost = cost,
            Status = "Pending"
        };
        var availability = _garageService.GetAvailableSpaces(model.GarageId);
        if (availability)
        {
            garage.ReminaingSpaces--;
            _garageService.Update(garage);
            _reservationService.Create(reservation);
        }

        return RedirectToAction("MyReservations");
    }

    // =========================
    // STEP 3: MY RESERVATIONS
    // =========================
    public IActionResult MyReservations()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return RedirectToAction("Login", "Account");

        var reservations = _reservationService.GetByCustomerId(userId.Value);

        return View(reservations);
    }

    // =========================
    // CANCEL BOOKING
    // =========================
    public IActionResult Cancel(int id)
    {
        var reservation = _reservationService.GetById(id);

        if (reservation != null)
        {
            reservation.Status = "Cancelled";
            _reservationService.Update(reservation);
        }

        return RedirectToAction("MyReservations");
    }

    // =========================
    // COST LOGIC
    // =========================
    private decimal CalculateCost(int garageId, DateTime startTime, DateTime endTime)
    {
        var totalHours = Math.Ceiling((endTime - startTime).TotalHours);
        
        if (totalHours <= 0)
            return 0;

        decimal hourlyRate;

        // Example dynamic pricing based on time of day
        if (startTime.Hour >= 8 && startTime.Hour < 18)
        {
            hourlyRate = 5.00m;
        }
        else
        {
            hourlyRate = 3.00m;
        }

        return (decimal)(totalHours) * hourlyRate;
    }
    public IActionResult Details(int id)
    {
        var reservation = _reservationService.GetById(id);

        if (reservation == null)
            return NotFound();

        return View(reservation);
    }
    
}