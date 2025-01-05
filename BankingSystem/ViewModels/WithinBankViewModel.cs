using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.ViewModels
{
    public class WithinBankViewModel
    {
        [Key]
        public string BenId { get; set; }
        public string BenName { get; set; }
        public string AccountNumber { get; set; }
        public string HolderName { get; set; }

        public BeneficiaryTransaction BenTransaction { get; set; }
    }
}
