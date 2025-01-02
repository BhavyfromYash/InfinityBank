using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.ViewModels
{
    public class TransactionViewModel
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } // "Deposit" or "Withdraw"
    }
}
