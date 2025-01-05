using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class FundTransferBeneficiary
    {
        [ForeignKey("BenId")]
        public Beneficiary Beneficiary { get; set; }

        [Key]
        public int FundsTransferBenId { get; set; }
        public string AccountNumber { get; set; }
        public int BenId { get; set; }
        public string ConfirmAccountNumber { get; set; }
        public string AccountType { get; set; } // Saving or Other
        public string IFSC { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string City { get; set; }
    }
}
