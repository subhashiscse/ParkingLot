using System.Collections.Generic;

public interface IVehicleService
{
    // Add new vehicle for a customer
    void Add(Vehicle vehicle);

    // Get vehicle by its Id
    Vehicle GetById(int id);

    // Get all vehicles for a specific customer
    List<Vehicle> GetByCustomerId(int customerId);

    // Update vehicle details
    void Update(Vehicle vehicle);

    // Delete vehicle
    void Delete(int id);
}