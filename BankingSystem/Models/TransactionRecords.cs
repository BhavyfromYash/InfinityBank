using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class TransactionRecords
    {
        [Key]
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}