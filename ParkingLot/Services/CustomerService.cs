using System;
using System.Collections.Generic;
using System.Linq;
using ParkingLot.Data;

namespace ParkingLot.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ParkingLotDbContext _context;

        public CustomerService(ParkingLotDbContext context)
        {
            _context = context;
        }

        // =========================
        // CREATE CUSTOMER
        // =========================
        public void Create(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            if (_context.Customers.Any(c => c.Email == customer.Email))
                throw new Exception("Email already exists");

            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        // =========================
        // GET BY ID
        // =========================
        public Customer GetById(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.CustomerId == id);
        }

        // =========================
        // GET BY EMAIL
        // =========================
        public Customer GetByEmail(string email)
        {
            Customer customer = _context.Customers.FirstOrDefault(c => c.Email == email);
            return customer;
        }

        // =========================
        // GET ALL
        // =========================
        public List<Customer> GetAll()
        {
            return _context.Customers.ToList().Where(c => c.CustomerType != "Admin").ToList();
        }

        // =========================
        // UPDATE CUSTOMER
        // =========================
        public void Update(Customer customer)
        {
            var existing = _context.Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);

            if (existing == null)
                throw new Exception("Customer not found");

            existing.Name = customer.Name;
            existing.PhoneNumber = customer.PhoneNumber;
            existing.BillingAddressLine = customer.BillingAddressLine;
            existing.City = customer.City;
            existing.Postcode = customer.Postcode;
            existing.IsActive = customer.IsActive;

            _context.SaveChanges();
        }

        // =========================
        // DELETE CUSTOMER
        // =========================
        public void Delete(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == id);

            if (customer == null)
                throw new Exception("Customer not found");

            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }
}