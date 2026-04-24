using System.ComponentModel.DataAnnotations;

public class VehicleViewModel
{
    public int VehicleId { get; set; }

    [Required]
    public string RegistrationNumber { get; set; } = string.Empty;

    public string Make { get; set; } = string.Empty;

    public string VehicleModel { get; set; } = string.Empty;

    public string Colour { get; set; } = string.Empty;
}