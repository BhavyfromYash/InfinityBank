using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Repository
{
    public class ManagerRepository : IManagerService
    {
        private readonly BankDbContext _context;

        public ManagerRepository(BankDbContext context)
        {
            _context = context;
        }

        public async Task<ManagerInfo> CreateManagerAsync(ManagerInfo managerInfo)
        { // Fetch the User details based on UserId and assign it to managerInfo.User
            managerInfo.User = await _context.Users.FindAsync(managerInfo.UserId);
            if (managerInfo.User == null)
            {
                throw new ArgumentException("Invalid UserId");
            }
            _context.ManagerInfos.Add(managerInfo);
            await _context.SaveChangesAsync();
            return managerInfo;
        }

        public async Task<IEnumerable<ManagerInfo>> GetAllManagersAsync()
        {
            return await _context.ManagerInfos.Include(m => m.User).ToListAsync();
        }

        public async Task<ManagerInfo> GetManagerByIdAsync(int id)
        {
            return await _context
                .ManagerInfos.Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<ManagerProfileViewModel> ViewManagerProfileAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserRole != "Manager")
            {
                return null;
            }
            var managerInfo = await _context.ManagerInfos.FirstOrDefaultAsync(m =>
                m.UserId == userId
            );
            if (managerInfo == null)
            {
                return null;
            }
            return new ManagerProfileViewModel
            {
                Name = user.Name,
                Email = user.Email,
                MobileNo = managerInfo.MobileNo,
                City = managerInfo.City,
                BranchName = managerInfo.BranchName,
                BranchAddress = managerInfo.BranchAddress,
            };
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> ApproveCustomerRequestAsync(int cusId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CusId == cusId);
            if (customer == null || customer.Status != "Pending")
                return null;
            customer.Status = "Approved";
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> RejectCustomerRequestAsync(int cusId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CusId == cusId);
            if (customer == null || customer.Status != "Pending")
                return null;
            customer.Status = "Rejected";
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<User> UnlockUserAccountAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                var accountStatus = await _context.UserAccountStatus.FirstOrDefaultAsync(s =>
                    s.UserId == user.UserId
                );
                if (accountStatus != null)
                {
                    accountStatus.IsLocked = false;
                    accountStatus.FailedLoginAttempts = 0;
                    await _context.SaveChangesAsync();
                }
            }
            return user;
        }

        public async Task<IEnumerable<Customer>> GetPendingApprovalsAsync()
        {
            return await _context.Customers.Where(c => c.Status == "Pending").ToListAsync();
        }
    }
}
