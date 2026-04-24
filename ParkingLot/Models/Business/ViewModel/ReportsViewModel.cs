using System;
using System.Collections.Generic;

public class ReportsViewModel
{
    // =========================
    // SUMMARY STATS
    // =========================
    public int TotalUsers { get; set; }
    public int TotalVehicles { get; set; }
    public int TotalBookings { get; set; }

    public int ActiveBookings { get; set; }
    public int CompletedBookings { get; set; }

    // =========================
    // DATE FILTER
    // =========================
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    // =========================
    // LIST DATA
    // =========================
    public List<Reservation> RecentReservations { get; set; }
    public List<Reservation> FilteredReservations { get; set; }

    public List<Customer> Users { get; set; }
    public List<Vehicle> Vehicles { get; set; }

    // =========================
    // CHART DATA (OPTIONAL)
    // =========================
    public List<string> ChartLabels { get; set; }     // e.g. Months
    public List<int> ChartData { get; set; }          // e.g. Booking count
}