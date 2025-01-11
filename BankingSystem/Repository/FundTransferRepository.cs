using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Repository
{
    public class FundTransferRepository : IFundTransferService
    {
        private readonly BankDbContext _context;
        private readonly ILogger<FundTransferRepository> _logger;

        public FundTransferRepository(BankDbContext context, ILogger<FundTransferRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Beneficiary> AddBeneficiaryAsync(Beneficiary beneficiary)
        {
            var user = await _context.Beneficiaries.AddAsync(beneficiary);
            await _context.SaveChangesAsync();
            return user.Entity;
        }

        public async Task<Beneficiary> GetBeneficiaryByIdAsync(int id)
        {
            return await _context.Beneficiaries.FirstOrDefaultAsync(b => b.BenId == id);
        }

        public async Task<Beneficiary> DeleteBeneficiaryAsync(int id)
        {
            var beneficiary = await _context.Beneficiaries.FindAsync(id);
            if (beneficiary != null)
            {
                _context.Beneficiaries.Remove(beneficiary);
            }
            await _context.SaveChangesAsync();

            return beneficiary;
        }

        public async Task<ShowAccountBalance> ShowAccountBalanceAsync(int userId)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == userId);
            if (account == null)
            {
                _logger.LogWarning("Account not found for user ID: {UserId}", userId);
                return null;
            }
            return new ShowAccountBalance
            {
                AccountNumber = account.AccountNumber,
                AvailableBalance = account.Balance,
            };
        }

        public async Task TransferFundsAsync(
            FundTransferBeneficiary fundTransferBeneficiary,
            decimal amount,
            string remarks
        )
        { // Validate Beneficiary Details
            var beneficiary = await _context.Beneficiaries.FindAsync(fundTransferBeneficiary.BenId);
            if (beneficiary == null)
            {
                throw new Exception("Beneficiary not found");
            } // Create Fund Transfer Transaction
            var fundTransfer = new FundTransfer
            {
                TransDate = DateTime.Now,
                TransMode = fundTransferBeneficiary.AccountType == "Within Bank" ? "NEFT" : "RTGS",
            }; // Save Transaction Details
            await _context.FundsTransfer.AddAsync(fundTransfer);
            await _context.SaveChangesAsync();
            // Update Beneficiary Transaction
            fundTransferBeneficiary.Beneficiary = beneficiary;
            await _context.FundTransferBeneficiaries.AddAsync(fundTransferBeneficiary);
            await _context.SaveChangesAsync();
        }

        // public async Task TransferFundsWithinBankAsync(
        //     WithinBankBeneficiary withinBankBeneficiary,
        //     decimal amount,
        //     string remarks,
        //     string transMode
        // )
        // {
        //     if (transMode != "NEFT" && transMode != "RTGS" && transMode != "IMPS")
        //     {
        //         throw new Exception("Invalid transaction mode");
        //     }
        //     var beneficiary = await _context.Beneficiaries.FindAsync(withinBankBeneficiary.BenId);
        //     if (beneficiary == null)
        //     {
        //         throw new Exception("Beneficiary not found");
        //     }
        //     var senderAccount = await _context.Accounts.FirstOrDefaultAsync(a =>
        //         a.AccountNumber == withinBankBeneficiary.AccountNumber
        //     );
        //     if (senderAccount == null)
        //     {
        //         throw new Exception("Sender account not found");
        //     }
        //     var receiverAccount = await _context.Accounts.FirstOrDefaultAsync(a =>
        //         a.HolderName == withinBankBeneficiary.Beneficiary.BenName
        //         && a.AccountNumber == withinBankBeneficiary.AccountNumber
        //     );
        //     if (receiverAccount == null)
        //     {
        //         throw new Exception("Receiver account not found within the same bank");
        //     }
        //     if (senderAccount.Balance < amount)
        //     {
        //         throw new Exception("Insufficient balance");
        //     }
        //     var fundTransfer = new FundTransfer { TransDate = DateTime.Now, TransMode = transMode };
        //     senderAccount.Balance -= amount;
        //     _context.Accounts.Update(senderAccount);
        //     receiverAccount.Balance += amount;
        //     _context.Accounts.Update(receiverAccount);
        //     await _context.FundsTransfer.AddAsync(fundTransfer);
        //     await _context.SaveChangesAsync();
        //     withinBankBeneficiary.Beneficiary = beneficiary;
        //     await _context.WithinBankBeneficiaries.AddAsync(withinBankBeneficiary);
        //     await _context.SaveChangesAsync();
        // }

        public async Task TransferFundsWithinBankAsync(
            int senderUserId,
            WithinBankBeneficiary withinBankBeneficiary,
            decimal amount,
            string remarks,
            string transMode
        )
        {
            _logger.LogInformation("Starting TransferFundsWithinBankAsync");

            if (transMode != "NEFT" && transMode != "RTGS" && transMode != "IMPS")
            {
                throw new Exception("Invalid transaction mode");
            }
            _logger.LogInformation("TransMode is valid: {TransMode}", transMode);

            var beneficiary = await _context.Beneficiaries.FindAsync(withinBankBeneficiary.BenId);
            if (beneficiary == null)
            {
                throw new Exception("Beneficiary not found");
            }
            _logger.LogInformation("Beneficiary found: {Beneficiary}", beneficiary.BenName);

            withinBankBeneficiary.Beneficiary = beneficiary;

            // Retrieve sender account using session-based UserId
            var senderAccount = await _context.Accounts.FirstOrDefaultAsync(a =>
                a.UserId == senderUserId
            );
            if (senderAccount == null)
            {
                throw new Exception("Sender account not found");
            }
            _logger.LogInformation(
                "Sender account found: {AccountNumber}, Balance: {Balance}",
                senderAccount.AccountNumber,
                senderAccount.Balance
            );

            // Ensure correct retrieval of receiver account based on beneficiary details
            var receiverAccount = await _context.Accounts.FirstOrDefaultAsync(a =>
                a.AccountNumber == withinBankBeneficiary.AccountNumber
            );
            if (receiverAccount == null)
            {
                throw new Exception("Receiver account not found within the same bank");
            }
            _logger.LogInformation(
                "Receiver account found: {AccountNumber}, Balance: {Balance}",
                receiverAccount.AccountNumber,
                receiverAccount.Balance
            );

            if (senderAccount.Balance < amount)
            {
                throw new Exception("Insufficient balance");
            }
            _logger.LogInformation("Sufficient balance available");

            var fundTransfer = new FundTransfer { TransDate = DateTime.Now, TransMode = transMode };
            _logger.LogInformation("Creating FundTransfer transaction");

            // Deduct amount from sender's account
            senderAccount.Balance -= amount;
            _context.Accounts.Attach(senderAccount);
            _context.Entry(senderAccount).Property(a => a.Balance).IsModified = true;
            _logger.LogInformation(
                "Deducted amount from sender's account. New balance: {Balance}",
                senderAccount.Balance
            );

            // Add amount to receiver's account
            receiverAccount.Balance += amount;
            _context.Accounts.Attach(receiverAccount);
            _context.Entry(receiverAccount).Property(a => a.Balance).IsModified = true;
            _logger.LogInformation(
                "Added amount to receiver's account. New balance: {Balance}",
                receiverAccount.Balance
            );

            // Save Transaction Details
            await _context.FundsTransfer.AddAsync(fundTransfer);
            await _context.WithinBankBeneficiaries.AddAsync(withinBankBeneficiary);

            // Ensure all changes are saved
            await _context.SaveChangesAsync();
            _logger.LogInformation("Saved all changes to the database");

            // Verification of balances
            var updatedSenderAccount = await _context.Accounts.FirstOrDefaultAsync(a =>
                a.AccountId == senderAccount.AccountId
            );
            var updatedReceiverAccount = await _context.Accounts.FirstOrDefaultAsync(a =>
                a.AccountId == receiverAccount.AccountId
            );
            _logger.LogInformation(
                "Verified Sender's updated balance: {Balance}",
                updatedSenderAccount.Balance
            );
            _logger.LogInformation(
                "Verified Receiver's updated balance: {Balance}",
                updatedReceiverAccount.Balance
            );
        }
    }
}
