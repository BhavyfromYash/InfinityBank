using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Services
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomerAsync(Customer newCustomer);
        Task<Customer> GetCustomerByIdAsync(int cusId);
    }
}
