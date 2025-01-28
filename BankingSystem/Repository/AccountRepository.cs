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

        // public async Task<Account> CreateAccountAsync(Account account)
        // {
        //     // Fetch the customer associated with the CusId
        //     var customer = await _context.Customers.FirstOrDefaultAsync(c =>
        //         c.CusId == account.CusId
        //     );

        //     if (customer == null)
        //     {
        //         // Log the error if customer is not found
        //         _logger.LogError("Customer not found for CusId: {CusId}", account.CusId);
        //         throw new Exception("Customer not found.");
        //     }

        //     account.Customer = customer; // Set the customer property

        //     _context.Accounts.Add(account);
        //     await _context.SaveChangesAsync();
        //     return account;
        // }

        // public async Task<Account> CreateAccountByIdAsync(int userId, NewAccountModel newAccount)
        // {
        //     // Check if user exists
        //     if (!await IsUserExistsAsync(userId))
        //     {
        //         throw new ArgumentException("User not found.");
        //     }

        //     // Check if customer exists
        //     var customer = await _context.Customers.SingleOrDefaultAsync(c =>
        //         c.CusId == newAccount.CusId
        //     );
        //     if (customer == null)
        //     {
        //         throw new ArgumentException("Customer not found.");
        //     }

        //     // Generate the account number
        //     var accountNumber = GenerateAccountNumber(newAccount.CusId);

        //     var account = new Account
        //     {
        //         HolderName = newAccount.HolderName,
        //         AccountNumber = accountNumber,
        //         CusId = newAccount.CusId,
        //         AccountType = newAccount.AccountType,
        //         IFSC = "INFB000UJ505", // Default IFSC for all customers
        //         BranchName = newAccount.BranchName,
        //         BranchAddress = newAccount.BranchAddress,
        //         BranchPhoneNo = newAccount.BranchPhoneNo,
        //         BranchEmailId = newAccount.BranchEmailId,
        //         Balance = newAccount.Balance,
        //         AccCreationDate = newAccount.AccCreationDate,
        //         UserId = userId,
        //         Customer = customer,
        //         User = await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId),
        //     };

        //     _context.Accounts.Add(account);
        //     await _context.SaveChangesAsync();

        //     return account;
        // }

        // private string GenerateAccountNumber(string cusId)
        // {
        //     var lastAccount = _context
        //         .Accounts.OrderByDescending(a => a.AccountNumber)
        //         .FirstOrDefault();

        //     if (lastAccount == null || lastAccount.AccountNumber.Length < 7)
        //     {
        //         return "1005501" + cusId;
        //     }

        //     var lastNumPart = int.Parse(lastAccount.AccountNumber.Substring(0, 7));
        //     var newNumPart = lastNumPart + 1;

        //     return newNumPart.ToString("D7") + cusId;
        // }

        // public async Task<bool> IsUserExistsAsync(int userId)
        // {
        //     return await _context.Users.AnyAsync(u => u.UserId == userId);
        // }

        // public async Task<Account> CreateAccountByIdAsync(int userId, NewAccountModel newAccount)
        // {
        //     // Check if user exists
        //     if (!await IsUserExistsAsync(userId))
        //     {
        //         throw new ArgumentException("User not found.");
        //     }

        //     // Check if customer exists
        //     var customer = await _context.Customers.SingleOrDefaultAsync(c =>
        //         c.CusId == newAccount.CusId
        //     );
        //     if (customer == null)
        //     {
        //         throw new ArgumentException("Customer not found.");
        //     }

        //     // Check customer status
        //     if (customer.Status == "Pending")
        //     {
        //         throw new InvalidOperationException("Customer approval is pending.");
        //     }
        //     if (customer.Status == "Rejected")
        //     {
        //         throw new InvalidOperationException("Customer request was rejected.");
        //     }

        //     // Generate the account number
        //     var accountNumber = GenerateAccountNumber(newAccount.CusId);

        //     var account = new Account
        //     {
        //         HolderName = newAccount.HolderName,
        //         AccountNumber = accountNumber,
        //         CusId = newAccount.CusId,
        //         AccountType = newAccount.AccountType,
        //         IFSC = "INFB000UJ505", // Default IFSC for all customers
        //         BranchName = newAccount.BranchName,
        //         BranchAddress = newAccount.BranchAddress,
        //         BranchPhoneNo = newAccount.BranchPhoneNo,
        //         BranchEmailId = newAccount.BranchEmailId,
        //         Balance = newAccount.Balance,
        //         AccCreationDate = newAccount.AccCreationDate,
        //         UserId = userId,
        //         Customer = customer,
        //         User = await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId),
        //     };

        //     _context.Accounts.Add(account);
        //     await _context.SaveChangesAsync();

        //     return account;
        // }

        // private string GenerateAccountNumber(string cusId)
        // {
        //     var lastAccount = _context
        //         .Accounts.OrderByDescending(a => a.AccountNumber)
        //         .FirstOrDefault();

        //     if (lastAccount == null || lastAccount.AccountNumber.Length < 7)
        //     {
        //         return "1005501" + cusId;
        //     }

        //     var lastNumPart = int.Parse(lastAccount.AccountNumber.Substring(0, 7));
        //     var newNumPart = lastNumPart + 1;

        //     return newNumPart.ToString("D7") + cusId;
        // }

        // public async Task<bool> IsUserExistsAsync(int userId)
        // {
        //     return await _context.Users.AnyAsync(u => u.UserId == userId);
        // }

        public async Task<Account> CreateAccountByIdAsync(int userId, NewAccountModel newAccount)
        {
            // Check if user exists
            if (!await IsUserExistsAsync(userId))
            {
                throw new ArgumentException("User not found.");
            }

            // Check if customer exists
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.CusId == newAccount.CusId);
            if (customer == null)
            {
                throw new ArgumentException("Customer not found.");
            }

            // Check customer status
            if (customer.Status == "Pending")
            {
                throw new InvalidOperationException("Customer approval is pending.");
            }
            if (customer.Status == "Rejected")
            {
                throw new InvalidOperationException("Customer request was rejected.");
            }

            // Check if customer already has a savings account
            var existingSavingsAccount = await _context.Accounts.FirstOrDefaultAsync(a =>
                a.CusId == newAccount.CusId && a.AccountType == "Savings"
            );
            if (existingSavingsAccount != null && newAccount.AccountType == "Savings")
            {
                throw new InvalidOperationException("Customer already has a savings account.");
            }

            // Generate the account number
            var accountNumber = GenerateAccountNumber(newAccount.CusId);

            var account = new Account
            {
                HolderName = newAccount.HolderName,
                AccountNumber = accountNumber,
                CusId = newAccount.CusId,
                AccountType = newAccount.AccountType,
                IFSC = "INFB000UJ505", // Default IFSC for all customers
                BranchName = newAccount.BranchName,
                BranchAddress = newAccount.BranchAddress,
                BranchPhoneNo = newAccount.BranchPhoneNo,
                BranchEmailId = newAccount.BranchEmailId,
                Balance = newAccount.Balance,
                AccCreationDate = newAccount.AccCreationDate,
                UserId = userId,
                Customer = customer,
                User = await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId),
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return account;
        }

        private string GenerateAccountNumber(string cusId)
        {
            var lastAccount = _context.Accounts.OrderByDescending(a => a.AccountNumber).FirstOrDefault();

            if (lastAccount == null || lastAccount.AccountNumber.Length < 7)
            {
                return "1005501" + cusId;
            }

            var lastNumPart = int.Parse(lastAccount.AccountNumber.Substring(0, 7));
            var newNumPart = lastNumPart + 1;

            return newNumPart.ToString("D7") + cusId;
        }

        public async Task<bool> IsUserExistsAsync(int userId)
        {
            return await _context.Users.AnyAsync(u => u.UserId == userId);
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

        // public async Task<bool> DepositByIdAsync(TransactionViewModel model)
        // {
        //     var account = await _context.Accounts.FirstOrDefaultAsync(a =>
        //         a.AccountId == model.AccountId
        //     );
        //     if (account == null)
        //         return false;
        //     var transaction = new Transaction
        //     {
        //         AccountId = model.AccountId,
        //         Amount = model.Amount,
        //         TransactionDate = DateTime.UtcNow.Date,
        //         TransactionType = "Deposit",
        //     };
        //     account.Balance += model.Amount;
        //     _context.Transactions.Add(transaction);
        //     _context.Accounts.Update(account);
        //     await _context.SaveChangesAsync();
        //     return true;
        // }

        // public async Task<(bool isSuccess, string message)> WithdrawByIdAsync(
        //     TransactionViewModel model,
        //     int userId
        // )
        // {
        //     _logger.LogInformation("Initiating withdrawal for user ID: {UserId}", userId);
        //     var account = await _context.Accounts.FirstOrDefaultAsync(a =>
        //         a.AccountId == model.AccountId && a.UserId == userId
        //     );

        //     if (account == null)
        //     {
        //         return (false, "Account not found.");
        //     }

        //     if (account.Balance < model.Amount)
        //     {
        //         return (false, "Insufficient balance.");
        //     }

        //     var transaction = new Transaction
        //     {
        //         AccountId = model.AccountId,
        //         Amount = model.Amount,
        //         TransactionDate = DateTime.UtcNow.Date,
        //         TransactionType = "Withdraw",
        //     };
        //     account.Balance -= model.Amount;

        //     _context.Transactions.Add(transaction);
        //     _context.Accounts.Update(account);
        //     await _context.SaveChangesAsync();

        //     _logger.LogInformation(
        //         "Withdrawal successful for AccountId: {AccountId}",
        //         model.AccountId
        //     );
        //     return (true, "Withdrawal successful.");
        // }

        public async Task<bool> DepositByIdAsync(TransactionViewModel model)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a =>
                a.AccountId == model.AccountId
            );
            if (account == null)
            {
                _logger.LogWarning("Account not found for AccountId: {AccountId}", model.AccountId);
                return false;
            }

            var transaction = new Transaction
            {
                AccountId = model.AccountId,
                Amount = model.Amount,
                TransactionDate = DateTime.UtcNow,
                TransactionType = "Deposit",
            };
            account.Balance += model.Amount;

            _context.Transactions.Add(transaction);
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<(bool isSuccess, string message)> WithdrawByIdAsync(
            TransactionViewModel model
        )
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a =>
                a.AccountId == model.AccountId
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
                TransactionDate = DateTime.UtcNow,
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

        // public async Task<ViewAccountStatement> ViewAccountStatementAsync(int userId)
        // {
        //     _logger.LogInformation("Retrieving account statement for user ID: {UserId}", userId);
        //     var account = await _context
        //         .Accounts.Include(a => a.Transactions)
        //         .FirstOrDefaultAsync(a => a.UserId == userId);
        //     if (account == null)
        //     {
        //         _logger.LogWarning("Account not found for user ID: {UserId}", userId);
        //         return null;
        //     }
        //     // Added transactionRecords with FromDate and ToDate
        //     var transactionRecords = new TransactionRecords
        //     {
        //         FromDate = DateTime.UtcNow.Date,
        //         ToDate = DateTime.UtcNow.Date,
        //     };
        //     return new ViewAccountStatement
        //     {
        //         AccountNumber = account.AccountNumber,
        //         HolderName = account.HolderName,
        //         AccountType = account.AccountType,
        //         Balance = account.Balance,
        //         transactionRecords = transactionRecords,

        //         Transactions = account
        //             .Transactions.Select(t => new TransactionViewModel
        //             {
        //                 AccountId = t.AccountId,
        //                 Amount = t.Amount,
        //                 TransactionType = t.TransactionType,
        //             })
        //             .ToList(),
        //     };
        // }

        // public async Task<ViewAccountStatement> ViewAccountStatementByIdAsync(int userId)
        // {
        //     _logger.LogInformation("Retrieving account statement for user ID: {UserId}", userId);

        //     var account = await _context.Accounts.Include(a => a.Transactions).FirstOrDefaultAsync(a => a.UserId == userId);

        //     if (account == null)
        //     {
        //         _logger.LogWarning("Account not found for user ID: {UserId}", userId);
        //         return null;
        //     }

        //     // Determine FromDate (latest) and ToDate (earliest) based on transaction records
        //     var fromDate = account.Transactions.Max(t => t.TransactionDate); // Latest date
        //     var toDate = account.Transactions.Min(t => t.TransactionDate); // Earliest date

        //     var transactionRecords = new TransactionRecords
        //     {
        //         FromDate = fromDate,
        //         ToDate = toDate
        //     };

        //     return new ViewAccountStatement
        //     {
        //         AccountNumber = account.AccountNumber,
        //         HolderName = account.HolderName,
        //         AccountType = account.AccountType,
        //         Balance = account.Balance,
        //         transactionRecords = transactionRecords,
        //         Transactions = account.Transactions.Select(t => new TransactionViewModel
        //         {
        //             AccountId = t.AccountId,
        //             Amount = t.Amount,
        //             TransactionType = t.TransactionType
        //         }).ToList(),
        //     };
        // }


        public async Task<ViewAccountStatement> ViewAccountStatementByIdAsync(int userId)
        {
            _logger.LogInformation("Retrieving account statement for user ID: {UserId}", userId);

            // Include the Transactions within the query to boost performance.
            var account = await _context
                .Accounts.Include(a => a.Transactions)
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (account == null)
            {
                _logger.LogWarning("Account not found for user ID: {UserId}", userId);
                return null;
            }

            // Ensure Transactions exist before attempting to find Min and Max dates.
            if (!account.Transactions.Any())
            {
                _logger.LogWarning("No transactions found for user ID: {UserId}", userId);
                return null;
            }

            // Retrieve the latest and earliest transaction dates.
            var fromDate = account.Transactions.Max(t => t.TransactionDate); // Latest date
            var toDate = account.Transactions.Min(t => t.TransactionDate); // Earliest date

            var transactionRecords = new TransactionRecords
            {
                FromDate = fromDate,
                ToDate = toDate,
            };

            // Map the account and transaction details to `ViewAccountStatement`.
            return new ViewAccountStatement
            {
                AccountNumber = account.AccountNumber,
                HolderName = account.HolderName,
                AccountType = account.AccountType,
                Balance = account.Balance,
                transactionRecords = transactionRecords,
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

        public async Task<ViewAccountStatement> ViewAccountStatementByDateRangeAsync(
            int userId,
            DateTime fromDate,
            DateTime toDate
        )
        {
            _logger.LogInformation(
                "Retrieving account statement for user ID: {UserId} within the date range from {FromDate} to {ToDate}",
                userId,
                fromDate,
                toDate
            );

            var account = await _context
                .Accounts.Include(a => a.Transactions)
                .FirstOrDefaultAsync(a =>
                    a.UserId == userId
                    && a.Transactions.Any(t =>
                        t.TransactionDate >= fromDate && t.TransactionDate <= toDate
                    )
                );

            if (account == null)
            {
                _logger.LogWarning(
                    "Account not found for user ID: {UserId} within the specified date range",
                    userId
                );
                return null;
            }

            var transactionRecords = new TransactionRecords
            {
                FromDate = fromDate,
                ToDate = toDate,
            };

            return new ViewAccountStatement
            {
                AccountNumber = account.AccountNumber,
                HolderName = account.HolderName,
                AccountType = account.AccountType,
                Balance = account.Balance,
                transactionRecords = transactionRecords,
                Transactions = account
                    .Transactions.Where(t =>
                        t.TransactionDate >= fromDate && t.TransactionDate <= toDate
                    )
                    .Select(t => new TransactionViewModel
                    {
                        AccountId = t.AccountId,
                        Amount = t.Amount,
                        TransactionType = t.TransactionType,
                    })
                    .ToList(),
            };
        }

        public async Task<AccountSummaryViewModel> GetAccountSummaryByIdAsync(int userId)
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

        public async Task<ShowAccountBalance> GetAccountBalanceAsync(int Id)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountId == Id);
            if (account == null)
            {
                _logger.LogWarning("Account not found for AccountId: {AccountId}", Id);
                return null;
            }
            return new ShowAccountBalance
            {
                AccountNumber = account.AccountNumber,
                AvailableBalance = account.Balance,
            };
        }

        public async Task<AccountDetailsViewModel> GetAccountDetailsByIdAsync(int Id)
        {
            var accountDetails = await _context
                .ViewAccountDetails.FromSqlInterpolated($"EXEC SP_GetAccountDetails {Id}")
                .ToListAsync();
            return accountDetails.FirstOrDefault();
        }
    }
}
