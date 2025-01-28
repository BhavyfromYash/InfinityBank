using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Services
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomerDetailsByIdAsync(
            int userId,
            CustomerCreationModel newCustomerDetails
        );
        Task<bool> IsUserExistsAsync(int userId);
        Task<Customer> GetCustomerByIdAsync(string cusId);
        Task<bool> IsCustomerDetailsUniqueAsync(string aadhaarNo, string panCardNo);

    }
}
