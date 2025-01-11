using System.Collections.Generic;
using System.Threading.Tasks;
using BankingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Repository
{
    public class CustomerRepository : ICustomerService
    {
        private readonly BankDbContext _context;

        public CustomerRepository(BankDbContext context)
        {
            _context = context;
        }

        // public async Task<Customer> CreateCustomerAsync(Customer newCustomer)
        // {
        //     newCustomer.Status = "Pending"; // Ensure status is set to Pending
        //     _context.Addresses.Add(newCustomer.Address);
        //     await _context.SaveChangesAsync();
        //     newCustomer.Address = newCustomer.Address;
        //     _context.Customers.AddAsync(newCustomer);
        //     await _context.SaveChangesAsync();
        //     return newCustomer;
        // }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        { // Add Address first and save changes to generate AddressId
            customer.Status = "Pending";
            // Add Address first and save changes to generate AddressId
            _context.Addresses.Add(customer.Address);
            await _context.SaveChangesAsync(); // Now AddressId is set, so we can add the customer
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> GetCustomerByIdAsync(int cusId)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.CusId == cusId);
        }
    }
}
