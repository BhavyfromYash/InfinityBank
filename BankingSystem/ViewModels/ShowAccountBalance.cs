using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.ViewModels
{
    public class ShowAccountBalance
    {
        public string AccountNumber { get; set; }
        public decimal AvailableBalance { get; set; }
    }
}
