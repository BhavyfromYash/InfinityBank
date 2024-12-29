using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class Beneficiary
    {
        [Key]
        public int BenId { get; set; }
        public string BenName { get; set; }
        public int BenAccNo { get; set; }
        public string IFSC { get; set; }
        public string BankName { get; set; }
        public int MobileNo { get; set; }
        public string Email { get; set; }
        public string BranchName { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
    }
}
