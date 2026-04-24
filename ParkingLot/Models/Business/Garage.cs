public class Garage
{
    public int GarageId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string AddressLine { get; set; } = string.Empty;
    public string Postcode { get; set; } = string.Empty; 
    public int ReminaingSpaces { get; set; }
    public int TotalSpaces { get; set; }
}