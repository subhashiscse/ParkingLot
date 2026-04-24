
using System.ComponentModel.DataAnnotations;

public class Vehicle
{
    public int VehicleId { get; set; }
    [Required] 
    public string RegistrationNumber { get; set; } = string.Empty;
    [Required]
    public string Make { get; set; }= string.Empty;
    [Required]
    public string VehicleModel { get; set; }= string.Empty;
    public string Colour { get; set; }= string.Empty;
    public bool IsDefault { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;

}