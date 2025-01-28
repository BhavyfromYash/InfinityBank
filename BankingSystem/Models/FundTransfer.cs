using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class FundTransfer
    {
        [Key]
        public int TransId { get; set; }
        public DateTime TransDate { get; set; }
        public string TransMode { get; set; } // NEFT , RTGS and IMPS

    }
}
