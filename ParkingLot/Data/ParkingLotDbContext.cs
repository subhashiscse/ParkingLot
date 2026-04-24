using Microsoft.EntityFrameworkCore;

namespace ParkingLot.Data
{
    public class ParkingLotDbContext : DbContext
    {
        public ParkingLotDbContext(DbContextOptions<ParkingLotDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Garage> Garages { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<MonthlyStatement> MonthlyStatements { get; set; }
    }
}