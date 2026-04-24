using Microsoft.AspNetCore.Mvc;

public class GarageController : Controller
{
    private readonly IGarageService _garageService;
    private readonly IReservationService _reservationService;

    public GarageController(IGarageService garageService,
        IReservationService reservationService)
    {
        _garageService = garageService;
        _reservationService = reservationService;
    }

    // =========================
    // LIST
    // =========================
    public IActionResult Index()
    {
        var garages = _garageService.GetAll();
        return View("~/Views/Manager/Garages.cshtml", garages);
    }

    // =========================
    // CREATE (GET)
    // =========================
    [HttpGet]
    public IActionResult AddGarage()
    {
        return View("~/Views/Manager/AddGarage.cshtml");
    }

    // =========================
    // CREATE (POST)
    // =========================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddGarage(Garage model)
    {
        if (!ModelState.IsValid)
            return View("~/Views/Manager/AddGarage.cshtml");
        model.ReminaingSpaces = model.TotalSpaces;
        _garageService.Create(model);
        return RedirectToAction("Index");
    }

    // =========================
    // EDIT (GET)
    // =========================
    [HttpGet]
    public IActionResult EditGarage(int id)
    {
        var garage = _garageService.GetById(id);

        if (garage == null)
            return NotFound();

        return View("~/Views/Manager/EditGarage.cshtml", garage);
    }

    // =========================
    // EDIT (POST)
    // =========================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditGarage(Garage model)
    {
        if (!ModelState.IsValid)
            return View("EditGarage", model);

        var existing = _garageService.GetById(model.GarageId);

        if (existing == null)
            return NotFound();

        existing.Name = model.Name;
        existing.City = model.City;
        existing.AddressLine = model.AddressLine;
        existing.Postcode = model.Postcode;
        existing.TotalSpaces = model.TotalSpaces;
        existing.ReminaingSpaces = existing.ReminaingSpaces>model.TotalSpaces?model.TotalSpaces:existing.ReminaingSpaces;

        _garageService.Update(existing);
        return RedirectToAction("Index");
    }

    // =========================
    // DELETE (GET - CONFIRM PAGE)
    // =========================
    [HttpGet]
    public IActionResult DeleteGarage(int id)
    {
        var garage = _garageService.GetById(id);

        if (garage == null)
            return NotFound();

        return View("~/Views/Manager/DeleteGarage.cshtml",garage);
    }

    // =========================
    // DELETE (POST - SAFE DELETE)
    // =========================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteGarageConfirmed(int garageId)
    {
        var garage = _garageService.GetById(garageId);

        if (garage == null)
            return NotFound();

        _garageService.Delete(garage.GarageId);
        _reservationService.DeleteAllByGarageID(garageId);
        return RedirectToAction("Index");
    }
}