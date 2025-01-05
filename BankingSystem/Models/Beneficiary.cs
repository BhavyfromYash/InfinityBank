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

        [Required]
        public string BenName { get; set; }

        [Required]
        public string MobileNo { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string TransferType { get; set; } // Within Bank and Other Bank
    }
}
