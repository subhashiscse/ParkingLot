using System;
using System.ComponentModel.DataAnnotations;

public class ReservationViewModel
{
    public int ReservationId { get; set; }

    [Required]
    public int GarageId { get; set; }

    public int? VehicleId { get; set; }

    [Required]
    public DateTime StartDateTime { get; set; }

    [Required]
    public DateTime EndDateTime { get; set; }

    public decimal EstimatedCharge { get; set; }
}