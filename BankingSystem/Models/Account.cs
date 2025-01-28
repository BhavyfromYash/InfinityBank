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
        public string AccountNumber { get; set; } // 1005501IB00501
        public string CusId { get; set; } // IB00501
        public string AccountType { get; set; }
        public string IFSC { get; set; } // INFB000UJ505
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string BranchPhoneNo { get; set; }
        public string BranchEmailId { get; set; }
        public decimal Balance { get; set; }
        public DateTime AccCreationDate { get; set; }
        public int UserId { get; set; } 

        // verify with CusId 

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
