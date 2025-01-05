using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string HolderName { get; set; }
        public string AccountNumber { get; set; }
        public int CusId { get; set; }
        public string AccountType { get; set; }
        public string IFSC { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public string BranchPhoneNo { get; set; }
        public string BranchEmailId { get; set; }
        public decimal Balance { get; set; }
        public DateTime AccCreationDate { get; set; }
        public int UserId { get; set; }

        // [ForeignKey("CusId")]
        // public Customer Customer { get; set; }
        // public ICollection<Transaction> Transactions { get; set; }

        [ForeignKey("CusId")]
        public Customer Customer { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
