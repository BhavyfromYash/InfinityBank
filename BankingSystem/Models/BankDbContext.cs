using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<LogOut> LogOuts { get; set; }
        public DbSet<ForgotPassword> ForgotPassword { get; set; }
        public DbSet<ManagerInfo> ManagerInfos { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<FundTransfer> FundsTransfer { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<ForgotUserId> ForgotUserId { get; set; }
        public DbSet<UserAccountStatus> UserAccountStatus { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<AccountDetailsViewModel> ViewAccountDetails { get; set; }
        public DbSet<FundTransferBeneficiary> FundTransferBeneficiaries { get; set; }
        public DbSet<WithinBankBeneficiary> WithinBankBeneficiaries { get; set; }
        public DbSet<OtherBankBeneficiary> OtherBankBeneficiaries { get; set; }
        public DbSet<TransactionRecords> transactionRecords { get; set; }
    }
}
