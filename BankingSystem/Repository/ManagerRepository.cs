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

        public async Task<Manager> CreateManagerAsync(Manager newManager)
        {
            await _context.Managers.AddAsync(newManager);
            await _context.SaveChangesAsync();
            return newManager;
        }

        // public async Task<IEnumerable<ManagerInfo>> GetAllManagersAsync()
        // {
        //     var result = await _context
        //         .Users.FromSqlRaw("EXEC SP_GetAllManagers")
        //         .AsEnumerable() // Perform composition on the client side
        //         .Select(u => new ManagerInfo
        //         {
        //             Name = u.Name,
        //             Email = u.Email,
        //             MobileNo = u.MobileNo,
        //             City = u.City,
        //             BranchName = u.BranchName,
        //             BranchAddress = u.BranchAddress,
        //         })
        //         .ToListAsync();
        //     return result;
        // }

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
