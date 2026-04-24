using System;
using System.Collections.Generic;
using System.Linq;
using ParkingLot.Data;

public class VehicleService : IVehicleService
{
    private readonly ParkingLotDbContext _context;

    public VehicleService(ParkingLotDbContext context)
    {
        _context = context;
    }

    // =========================
    // ADD VEHICLE
    // =========================
    public void Add(Vehicle vehicle)
    {
        if (vehicle == null)
            throw new ArgumentNullException(nameof(vehicle));

        // prevent duplicate registration
        if (_context.Vehicles.Any(v => v.RegistrationNumber == vehicle.RegistrationNumber))
            throw new Exception("Vehicle already exists");

        _context.Vehicles.Add(vehicle);
        _context.SaveChanges();
    }

    // =========================
    // GET BY ID
    // =========================
    public Vehicle GetById(int id)
    {
        return _context.Vehicles.FirstOrDefault(v => v.VehicleId == id);
    }

    // =========================
    // GET BY CUSTOMER
    // =========================
    public List<Vehicle> GetByCustomerId(int customerId)
    {
        return _context.Vehicles
            .Where(v => v.CustomerId == customerId)
            .ToList();
    }

    // =========================
    // UPDATE VEHICLE
    // =========================
    public void Update(Vehicle vehicle)
    {
        var existing = _context.Vehicles.FirstOrDefault(v => v.VehicleId == vehicle.VehicleId);

        if (existing == null)
            throw new Exception("Vehicle not found");

        existing.RegistrationNumber = vehicle.RegistrationNumber;
        existing.Make = vehicle.Make;
        existing.VehicleModel = vehicle.VehicleModel;
        existing.Colour = vehicle.Colour;
        existing.IsDefault = vehicle.IsDefault;

        _context.SaveChanges();
    }

    // =========================
    // DELETE VEHICLE
    // =========================
    public void Delete(int id)
    {
        var vehicle = _context.Vehicles.FirstOrDefault(v => v.VehicleId == id);

        if (vehicle == null)
            throw new Exception("Vehicle not found");

        _context.Vehicles.Remove(vehicle);
        _context.SaveChanges();
    }
}