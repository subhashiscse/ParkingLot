using Microsoft.AspNetCore.Mvc;

public class ReportsController : Controller
{
    private readonly IReportsService _reportsService;

    public ReportsController(IReportsService reportsService)
    {
        _reportsService = reportsService;
    }

    // =========================
    // MAIN REPORT PAGE
    // =========================
    public IActionResult Index()
    {
        var model = _reportsService.GetSummaryStats();
        return View("~/Views/Manager/Reports.cshtml", model);
    }

    // =========================
    // BOOKINGS LIST
    // =========================
    public IActionResult Bookings()
    {
        var bookings = _reportsService.GetAllBookings();
        return View("~/Views/Manager/ReportsBookings.cshtml", bookings);
    }

    // =========================
    // USERS LIST
    // =========================
    public IActionResult Users()
    {
        var users = _reportsService.GetAllUsers();
        return View("~/Views/Manager/ReportsUsers.cshtml", users);
    }

    // =========================
    // VEHICLES LIST
    // =========================
    public IActionResult Vehicles()
    {
        var vehicles = _reportsService.GetAllVehicles();
        return View("~/Views/Manager/ReportsVehicles.cshtml", vehicles);
    }
}