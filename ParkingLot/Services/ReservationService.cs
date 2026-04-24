using System;
using System.Collections.Generic;
using System.Linq;
using ParkingLot.Data;

public class ReservationService : IReservationService
{
    private readonly ParkingLotDbContext _context;

    public ReservationService(ParkingLotDbContext context)
    {
        _context = context;
    }

    // =========================
    // CREATE
    // =========================
    public void Create(Reservation reservation)
    {
        _context.Reservations.Add(reservation);
        _context.SaveChanges();
    }

    // =========================
    // GET BY ID
    // =========================
    public Reservation GetById(int id)
    {
        return _context.Reservations
            .FirstOrDefault(r => r.ReservationId == id);
    }

    // =========================
    // GET BY CUSTOMER
    // =========================
    public List<Reservation> GetByCustomerId(int customerId)
    {
        return _context.Reservations
            .Where(r => r.CustomerId == customerId)
            .OrderByDescending(r => r.StartDateTime)
            .ToList();
    }

    // =========================
    // GET BY GARAGE
    // =========================
    public List<Reservation> GetByGarageId(int garageId)
    {
        return _context.Reservations
            .Where(r => r.GarageId == garageId)
            .ToList();
    }

    // =========================
    // UPDATE
    // =========================
    public void Update(Reservation reservation)
    {
        _context.Reservations.Update(reservation);
        _context.SaveChanges();
    }

    // =========================
    // CANCEL (SOFT DELETE)
    // =========================
    public void Cancel(int reservationId)
    {
        var reservation = _context.Reservations
            .FirstOrDefault(r => r.ReservationId == reservationId);

        if (reservation != null)
        {
            reservation.Status = "Cancelled";
            _context.SaveChanges();
        }
    }

    // =========================
    // DELETE (HARD DELETE)
    // =========================
    public void Delete(int reservationId)
    {
        var reservation = _context.Reservations
            .FirstOrDefault(r => r.ReservationId == reservationId);

        if (reservation != null)
        {
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
        }
    }

    public List<Reservation> GetAll()
    {
        return _context.Reservations.ToList();
    }

    public void DeleteAllByGarageID(int garageId)
    {
        _context.Reservations.RemoveRange(_context.Reservations.Where(r => r.GarageId == garageId));
        return;
    }
}