using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.ViewModels
{
    public class CreateBeneficiaryTransaction
    {
        public int BenId { get; set; }
        public string AccountNumber { get; set; }
        public string BeneficiaryName { get; set; }
        public string IFSC { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string City { get; set; }
        public BeneficiaryTransaction BenTransaction { get; set; }
    }
}
