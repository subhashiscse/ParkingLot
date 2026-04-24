using Microsoft.AspNetCore.Mvc;

public class ParkingController : Controller
{
    private readonly IGarageService _garageService;

    public ParkingController(IGarageService garageService)
    {
        _garageService = garageService;
    }

    // =========================
    // FIND GARAGE PAGE (ENTRY POINT)
    // =========================
    [HttpGet]
    public IActionResult FindGarage(string city)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return RedirectToAction("Login", "Account");

        List<Garage> garages;

        // 🔍 Search or Get All
        if (string.IsNullOrWhiteSpace(city))
        {
            garages = _garageService.GetAll()
                        .ToList();
        }
        else
        {
            garages = _garageService.Search(city, null)
                        .ToList();
        }

        return View(garages);
        // Views/Parking/FindGarage.cshtml
    }

    // =========================
    // GARAGE DETAILS PAGE (OPTIONAL)
    // =========================
    public IActionResult GarageDetails(int id)
    {
        var garage = _garageService.GetById(id);

        if (garage == null)
            return NotFound();

        return View(garage);
        // Views/Parking/GarageDetails.cshtml
    }

    // =========================
    // REDIRECT TO BOOKING FLOW
    // =========================
    public IActionResult BookNow(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return RedirectToAction("Login", "Account");

        return RedirectToAction("Book", "Reservation", new { garageId = id });
    }
}