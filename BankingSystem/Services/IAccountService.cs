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
        Task<Account> GetAccountByUserIdAsync(int userId);
        Task<ViewAccountStatement> ViewAccountStatementAsync(int userId);
        Task<bool> DepositAsync(TransactionViewModel model);
        Task<(bool isSuccess, string message)> WithdrawAsync(
            TransactionViewModel model,
            int UserId
        );
        Task<AccountSummaryViewModel> GetAccountSummaryAsync(int userId);
        Task<AccountDetailsViewModel> GetAccountDetailsAsync(int userId);
    }
}
