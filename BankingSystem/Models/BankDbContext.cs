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
        public DbSet<Address> Address { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<NetUser> NetUsers { get; set; }
        public DbSet<LogOut> LogOuts { get; set; }
        public DbSet<ForgotPassword> ForgotPassword { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<FundTransfer> FundsTransfer { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<ForgotUserId> ForgotUserId { get; set; }
        public DbSet<UserAccountStatus> UserAccountStatus { get; set; }
    }
}
