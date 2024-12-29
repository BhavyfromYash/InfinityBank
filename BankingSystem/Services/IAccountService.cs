using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Services
{
    public interface IAccountService
    {
        Task<Account> CreateAccountAsync(Account newAccount);
        Task<Account> GetAccountByIdAsync(int accountId);
    }
}
