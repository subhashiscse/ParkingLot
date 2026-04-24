using System.Collections.Generic;

public interface ICustomerService
{
    // Create new customer
    void Create(Customer customer);

    // Get customer by id
    Customer GetById(int id);

    // Get customer by email (used for login + validation)
    Customer GetByEmail(string email);

    // Get all customers (optional admin use)
    List<Customer> GetAll();

    // Update customer profile (name, address, etc.)
    void Update(Customer customer);

    // Delete customer (optional admin feature)
    void Delete(int id);

}