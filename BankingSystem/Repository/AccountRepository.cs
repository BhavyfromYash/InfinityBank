using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Repository
{
    public class AccountRepository : IAccountService
    {
        private readonly BankDbContext _context;

        public AccountRepository(BankDbContext context)
        {
            _context = context;
        }

        public async Task<Account> CreateAccountAsync(Account newAccount)
        {
            await _context.Accounts.AddAsync(newAccount);
            await _context.SaveChangesAsync();
            return newAccount;
        }

        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            return await _context
                .Accounts.Include(a => a.Customer)
                .FirstOrDefaultAsync(a => a.AccountId == accountId);
        }
    }
}
