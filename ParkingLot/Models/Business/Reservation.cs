public class Reservation
{
    public int ReservationId { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int GarageId { get; set; }
    public string GarageName { get; set; }= string.Empty;
    public int? VehicleId { get; set; }
    public string VehicleName { get; set; } = string.Empty;
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal Cost { get; set; }
}