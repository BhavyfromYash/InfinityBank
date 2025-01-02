using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Services
{
    public interface IManagerService
    {
        Task<ManagerInfo> CreateManagerAsync(ManagerInfo newManager);

        Task<IEnumerable<ManagerInfo>> GetAllManagersAsync();
        Task<ManagerInfo> GetManagerByIdAsync(int id);
        Task<ManagerProfileViewModel> ViewManagerProfileAsync(int userId);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> ApproveCustomerRequestAsync(int cusId);
        Task<Customer> RejectCustomerRequestAsync(int cusId);
        Task<IEnumerable<Customer>> GetPendingApprovalsAsync();
        Task<User> UnlockUserAccountAsync(int userId);
    }
}
