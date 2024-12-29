using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Services
{
    public interface IManagerService
    {
        Task<Manager> CreateManagerAsync(Manager newManager);

        // Task<IEnumerable<ManagerInfo>> GetAllManagersAsync();

        // Task<Manager> ViewProfileAsync(int Id);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> ApproveCustomerRequestAsync(int cusId);
        Task<Customer> RejectCustomerRequestAsync(int cusId);
        Task<IEnumerable<Customer>> GetPendingApprovalsAsync();
        Task<User> UnlockUserAccountAsync(int userId);
    }
}
