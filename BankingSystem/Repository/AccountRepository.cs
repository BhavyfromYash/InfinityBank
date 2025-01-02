// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace BankingSystem.Repository
// {
//     public class AccountRepository : IAccountService
//     {
//         private readonly BankDbContext _context;

//         public AccountRepository(BankDbContext context)
//         {
//             _context = context;
//         }

//         public async Task<Account> CreateAccountAsync(Account newAccount)
//         {
//             await _context.Accounts.AddAsync(newAccount);
//             await _context.SaveChangesAsync();
//             return newAccount;
//         }

//         public async Task<Account> GetAccountByIdAsync(int accountId)
//         {
//             return await _context
//                 .Accounts.Include(a => a.Customer)
//                 .FirstOrDefaultAsync(a => a.AccountId == accountId);
//         }

//         public async Task<ViewAccountStatement> ViewAccountStatementAsync(int userId)
//         { // Retrieve the account based on userId
//             var account = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == userId);
//             if (account == null)
//                 return null;
//             return new ViewAccountStatement
//             {
//                 AccountNumber = account.AccountNumber,
//                 HolderName = account.HolderName,
//                 ToDate = account.ToDate,
//                 FromDate = account.FromDate,
//                 AccountType = account.AccountType,
//                 Balance = account.Balance,
//             };
//         }
//     }
// }

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

        public AccountRepository(BankDbContext context)
        {
            _context = context;
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            return await _context
                .Accounts.Include(a => a.Transactions) // Include transactions
                .FirstOrDefaultAsync(a => a.AccountId == accountId);
        }

        // public async Task<Account> GetAccountByUserIdAsync(int userId)
        // {
        //     return await _context
        //         .Accounts.Include(a => a.Transactions) // Include transactions if needed
        //         .FirstOrDefaultAsync(a => a.UserId == userId);
        // }

        // public async Task<ViewAccountStatement> ViewAccountStatementAsync(int userId)
        // {
        //     var account = await _context
        //         .Accounts.Include(a => a.Transactions)
        //         .FirstOrDefaultAsync(a => a.UserId == userId);
        //     if (account == null)
        //     {
        //         // Add logging here
        //         Console.WriteLine("Account not found for UserId: " + userId);
        //         return null;
        //     }

        //     return new ViewAccountStatement
        //     {
        //         AccountNumber = account.AccountNumber,
        //         HolderName = account.HolderName,
        //         ToDate = account.ToDate,
        //         FromDate = account.FromDate,
        //         AccountType = account.AccountType,
        //         Balance = account.Balance,
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

        // public async Task<bool> DepositAsync(TransactionViewModel model)
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
        //         TransactionDate = DateTime.UtcNow,
        //         TransactionType = "Deposit",
        //     };

        //     account.Balance += model.Amount;
        //     _context.Transactions.Add(transaction);
        //     _context.Accounts.Update(account);
        //     await _context.SaveChangesAsync();

        //     return true;
        // }

        // public async Task<bool> WithdrawAsync(TransactionViewModel model)
        // {
        //     var account = await _context.Accounts.FirstOrDefaultAsync(a =>
        //         a.AccountId == model.AccountId
        //     );

        //     if (account == null)
        //     {
        //         // Add logging here
        //         Console.WriteLine("Account not found for AccountId: " + model.AccountId);
        //         return false;
        //     }

        //     if (account.Balance < model.Amount)
        //     {
        //         // Add logging here
        //         Console.WriteLine("Insufficient balance for AccountId: " + model.AccountId);
        //         return false;
        //     }

        //     var transaction = new Transaction
        //     {
        //         AccountId = model.AccountId,
        //         Amount = model.Amount,
        //         TransactionDate = DateTime.UtcNow,
        //         TransactionType = "Withdraw",
        //     };

        //     account.Balance -= model.Amount;
        //     _context.Transactions.Add(transaction);
        //     _context.Accounts.Update(account);
        //     await _context.SaveChangesAsync();

        //     return true;
        // }

        public async Task<Account> GetAccountByUserIdAsync(int userId)
        {
            return await _context
                .Accounts.Include(a => a.Transactions) // Include transactions if needed
                .FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task<ViewAccountStatement> ViewAccountStatementAsync(int userId)
        {
            var account = await _context
                .Accounts.Include(a => a.Transactions)
                .FirstOrDefaultAsync(a => a.UserId == userId);
            if (account == null)
                return null;
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
                TransactionDate = DateTime.UtcNow,
                TransactionType = "Deposit",
            };
            account.Balance += model.Amount;
            _context.Transactions.Add(transaction);
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> WithdrawAsync(TransactionViewModel model)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a =>
                a.AccountId == model.AccountId
            );
            if (account == null || account.Balance < model.Amount)
                return false;
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
            return true;
        }
    }
}
