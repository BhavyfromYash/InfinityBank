using System;
using System.Threading.Tasks;
using BankingSystem.Models;
using BankingSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Services
{
    public class AccountRepository : IAccountService
    {
        private readonly BankDbContext _context;
        private readonly ILogger<AccountRepository> _logger;

        public AccountRepository(BankDbContext context, ILogger<AccountRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            // Fetch the customer associated with the CusId
            var customer = await _context.Customers.FirstOrDefaultAsync(c =>
                c.CusId == account.CusId
            );

            if (customer == null)
            {
                // Log the error if customer is not found
                _logger.LogError("Customer not found for CusId: {CusId}", account.CusId);
                throw new Exception("Customer not found.");
            }

            account.Customer = customer; // Set the customer property

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            return await _context
                .Accounts.Include(a => a.Customer) // Include transactions
                .FirstOrDefaultAsync(a => a.AccountId == accountId);
        }

        public async Task<Account> GetAccountByUserIdAsync(int userId)
        {
            return await _context
                .Accounts.Include(a => a.Transactions) // Include transactions if needed
                .FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task<bool> DepositAsync(TransactionViewModel model)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a =>
                a.AccountId == model.AccountId
            );
            if (account == null)
                return false;
            var transaction = new Transaction
            {
                AccountId = model.AccountId,
                Amount = model.Amount,
                TransactionDate = DateTime.UtcNow.Date,
                TransactionType = "Deposit",
            };
            account.Balance += model.Amount;
            _context.Transactions.Add(transaction);
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<(bool isSuccess, string message)> WithdrawAsync(
            TransactionViewModel model,
            int userId
        )
        {
            _logger.LogInformation("Initiating withdrawal for user ID: {UserId}", userId);
            var account = await _context.Accounts.FirstOrDefaultAsync(a =>
                a.AccountId == model.AccountId && a.UserId == userId
            );

            if (account == null)
            {
                return (false, "Account not found.");
            }

            if (account.Balance < model.Amount)
            {
                return (false, "Insufficient balance.");
            }

            var transaction = new Transaction
            {
                AccountId = model.AccountId,
                Amount = model.Amount,
                TransactionDate = DateTime.UtcNow.Date,
                TransactionType = "Withdraw",
            };
            account.Balance -= model.Amount;

            _context.Transactions.Add(transaction);
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Withdrawal successful for AccountId: {AccountId}",
                model.AccountId
            );
            return (true, "Withdrawal successful.");
        }

        public async Task<ViewAccountStatement> ViewAccountStatementAsync(int userId)
        {
            _logger.LogInformation("Retrieving account statement for user ID: {UserId}", userId);
            var account = await _context
                .Accounts.Include(a => a.Transactions)
                .FirstOrDefaultAsync(a => a.UserId == userId);
            if (account == null)
            {
                _logger.LogWarning("Account not found for user ID: {UserId}", userId);
                return null;
            }
            return new ViewAccountStatement
            {
                AccountNumber = account.AccountNumber,
                HolderName = account.HolderName,
                ToDate = account.ToDate,
                FromDate = account.FromDate,
                AccountType = account.AccountType,
                Balance = account.Balance,
                Transactions = account
                    .Transactions.Select(t => new TransactionViewModel
                    {
                        AccountId = t.AccountId,
                        Amount = t.Amount,
                        TransactionType = t.TransactionType,
                    })
                    .ToList(),
            };
        }

        public async Task<AccountSummaryViewModel> GetAccountSummaryAsync(int userId)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == userId);
            if (account == null)
            {
                _logger.LogWarning("Account not found for user ID: {UserId}", userId);
                return null;
            }
            return new AccountSummaryViewModel
            {
                CusId = account.CusId,
                HolderName = account.HolderName,
                BranchName = account.BranchName,
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
            };
        }

        public async Task<AccountDetailsViewModel> GetAccountDetailsAsync(int Id)
        {
            var accountDetails = await _context
                .ViewAccountDetails.FromSqlInterpolated($"EXEC SP_GetAccountDetails {Id}")
                .ToListAsync();
            return accountDetails.FirstOrDefault();
        }
    }
}
