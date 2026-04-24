using System.Collections.Generic;

public interface IGarageService
{
    // Create new garage
    void Create(Garage garage);

    // Get garage by Id
    Garage GetById(int id);

    // Get all garages
    List<Garage> GetAll();

    // Update garage details
    void Update(Garage garage);

    // Delete garage
    void Delete(int id);

    // Get available parking spaces count (important for booking logic)
    bool GetAvailableSpaces(int garageId);
    
    List<Garage> Search(string city, string postcode);
}