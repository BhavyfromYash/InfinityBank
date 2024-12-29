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

        public async Task<Customer> CreateCustomerAsync(Customer newCustomer)
        {
            newCustomer.Status = "Pending"; // Ensure status is set to Pending
            await _context.Customers.AddAsync(newCustomer);
            await _context.SaveChangesAsync();
            return newCustomer;
        }

        public async Task<Customer> GetCustomerByIdAsync(int cusId)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.CusId == cusId);
        }
    }
}
