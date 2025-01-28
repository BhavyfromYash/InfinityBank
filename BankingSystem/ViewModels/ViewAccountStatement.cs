using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.ViewModels
{
    public class ViewAccountStatement
    {
        public string AccountNumber { get; set; }
        public string HolderName { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        
        public TransactionRecords transactionRecords{get; set;}
        public List<TransactionViewModel> Transactions { get; set; }
    }
}
