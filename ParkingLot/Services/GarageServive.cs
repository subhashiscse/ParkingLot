using System.Collections.Generic;
using System.Linq;
using ParkingLot.Data;

public class GarageService : IGarageService
{
    private readonly ParkingLotDbContext _context;

    public GarageService(ParkingLotDbContext context)
    {
        _context = context;
    }

    // =========================
    // CREATE
    // =========================
    public void Create(Garage garage)
    {
        _context.Garages.Add(garage);
        _context.SaveChanges();
    }

    // =========================
    // GET BY ID
    // =========================
    public Garage GetById(int id)
    {
        return _context.Garages.FirstOrDefault(g => g.GarageId == id);
    }

    // =========================
    // GET ALL
    // =========================
    public List<Garage> GetAll()
    {
        return _context.Garages.ToList();
    }

    // =========================
    // UPDATE
    // =========================
    public void Update(Garage garage)
    {
        _context.Garages.Update(garage);
        _context.SaveChanges();
    }

    // =========================
    // DELETE
    // =========================
    public void Delete(int id)
    {
        var garage = _context.Garages.FirstOrDefault(g => g.GarageId == id);

        if (garage != null)
        {
            _context.Garages.Remove(garage);
            _context.SaveChanges();
        }
    }

    // =========================
    // AVAILABLE SPACES
    // =========================
    public bool GetAvailableSpaces(int garageId)
    {
        return _context.Garages.Any(p => p.GarageId == garageId && p.ReminaingSpaces>0);
    }
    public List<Garage> Search(string city, string postcode)
    {
        var query = _context.Garages.AsQueryable();

        if (!string.IsNullOrEmpty(city))
        {
            query = query.Where(g => g.City.Contains(city));
        }

        if (!string.IsNullOrEmpty(postcode))
        {
            query = query.Where(g => g.Postcode.Contains(postcode));
        }

        return query.ToList();
    }
}