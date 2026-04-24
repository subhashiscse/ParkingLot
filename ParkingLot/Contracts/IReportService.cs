using System.Collections.Generic;

public interface IReportsService
{
    ReportsViewModel GetSummaryStats();

    List<Reservation> GetAllBookings();

    List<Customer> GetAllUsers();

    List<Vehicle> GetAllVehicles();
}