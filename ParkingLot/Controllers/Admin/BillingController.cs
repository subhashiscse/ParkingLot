using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

public class BillingController : Controller
{
    private readonly IBillingService _billingService;

    public BillingController(IBillingService billingService)
    {
        _billingService = billingService;
    }

    // =========================
    // STATEMENT LIST (Customer)
    // =========================
    public IActionResult Statements()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return RedirectToAction("Login", "Account");

        var statements = _billingService.GetByCustomerId(userId.Value);

        return View(statements);
    }

    // =========================
    // STATEMENT DETAILS
    // =========================
    public IActionResult StatementDetails(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return RedirectToAction("Login", "Account");

        var statement = _billingService.GetById(id);

        if (statement == null)
            return NotFound();

        return View(statement);
    }
}