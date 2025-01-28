using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Services
{
    public interface IAccountService
    {
        Task<Account> CreateAccountByIdAsync(int userId, NewAccountModel newAccount);
        Task<Account> GetAccountByIdAsync(int accountId);
        Task<Account> GetAccountByUserIdAsync(int userId);
        Task<ViewAccountStatement> ViewAccountStatementByIdAsync(int userId);
        Task<ViewAccountStatement> ViewAccountStatementByDateRangeAsync(
            int userId,
            DateTime fromDate,
            DateTime toDate
        );
        Task<bool> IsUserExistsAsync(int userId);

        Task<bool> DepositByIdAsync(TransactionViewModel accountId);
        Task<(bool isSuccess, string message)> WithdrawByIdAsync(
            TransactionViewModel accountId

        );
        Task<AccountSummaryViewModel> GetAccountSummaryByIdAsync(int userId);
        Task<AccountDetailsViewModel> GetAccountDetailsByIdAsync(int userId);

        Task<ShowAccountBalance> GetAccountBalanceAsync(int accountId);
    }
}
