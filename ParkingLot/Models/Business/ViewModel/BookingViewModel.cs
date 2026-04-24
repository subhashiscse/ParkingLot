using System;
using System.Collections.Generic;

public class BookingViewModel
{
    public int GarageId { get; set; }
    public int VehicleId { get; set; }
    public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public decimal Cost { get; set; }
}