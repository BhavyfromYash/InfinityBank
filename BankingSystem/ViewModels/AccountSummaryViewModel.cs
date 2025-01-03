using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.ViewModels
{
    public class AccountSummaryViewModel
    {
        public int CusId { get; set; }
        public string HolderName { get; set; }
        public string BranchName { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
