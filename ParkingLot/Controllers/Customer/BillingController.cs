using Microsoft.AspNetCore.Mvc;

namespace ParkingLot.Controllers
{
    public class BillingController : Controller
    {
        private readonly IBillingService _billingService;
        private readonly IReservationService _reservationService;
        private readonly IGarageService _garageService;

        public BillingController(IBillingService billingService,
            IReservationService reservationService,
            IGarageService garageService)
        {
            _billingService = billingService;
            _reservationService = reservationService;
            _garageService = garageService;
        }

        // =========================
        // STATEMENT LIST
        // =========================
        public IActionResult Statement()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var statements = _billingService.GetByCustomerId(userId.Value);

            return View(statements);
        }

        // =========================
        // DETAILS
        // =========================
        public IActionResult StateDetails(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var statement = _billingService.GetById(id);

            if (statement == null)
                return NotFound();

            return View(statement);
        }
        [HttpPost]
        public IActionResult ConfirmPayment(int reservationId)
        {
            var reservation = _reservationService.GetById(reservationId);

            if (reservation == null)
                return NotFound();

            var statement = new MonthlyStatement
            {
                ReservationId = reservationId,
                Amount = reservation.Cost,
                CustomerId = reservation.CustomerId,
                CustomerName = reservation.CustomerName,
                PaymentDate =  DateTime.Now,
            };
            reservation.Status = "Paid";
            _reservationService.Update(reservation);

            _billingService.Create(statement);
            var garage  = _garageService.GetById(reservation.GarageId);
            garage.ReminaingSpaces++;
            _garageService.Update(garage);

            return RedirectToAction("MyReservations", "Reservation");
        }
    }
}