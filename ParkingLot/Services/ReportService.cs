using System;
using System.Collections.Generic;
using System.Linq;
using ParkingLot.Data;

public class ReportsService : IReportsService
{
    private readonly ParkingLotDbContext _context;

    public ReportsService(ParkingLotDbContext context)
    {
        _context = context;
    }

    // =========================
    // DASHBOARD SUMMARY
    // =========================
    public ReportsViewModel GetSummaryStats()
    {
        var model = new ReportsViewModel
        {
            TotalUsers = _context.Customers.Count(c=>c.CustomerType != "Admin"),
            TotalVehicles = _context.Vehicles.Count(),
            TotalBookings = _context.Reservations.Count(),

            ActiveBookings = _context.Reservations
                .Count(r => r.Status == "Progress"),

            CompletedBookings = _context.Reservations
                .Count(r => r.Status == "Completed")
        };

        return model;
    }

    // =========================
    // GET ALL BOOKINGS
    // =========================
    public List<Reservation> GetAllBookings()
    {
        return _context.Reservations.ToList();
    }

    // =========================
    // GET ALL USERS
    // =========================
    public List<Customer> GetAllUsers()
    {
        return _context.Customers.ToList().Where(c=>c.CustomerType != "Admin").ToList();
    }

    // =========================
    // GET ALL VEHICLES
    // =========================
    public List<Vehicle> GetAllVehicles()
    {
        return _context.Vehicles.ToList();
    }
}