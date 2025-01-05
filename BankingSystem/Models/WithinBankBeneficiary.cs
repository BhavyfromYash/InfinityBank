using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class WithinBankBeneficiary
    {
        [ForeignKey("BenId")]
        public Beneficiary Beneficiary { get; set; }

        [Key]
        public int WithinBankBenId { get; set; }
        public string AccountNumber { get; set; }
        public int BenId { get; set; }
    }
}
