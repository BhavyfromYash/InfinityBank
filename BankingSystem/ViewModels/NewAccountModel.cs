using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.ViewModels
{
    public class NewAccountModel
    {
        [Required]
        public string HolderName { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string CusId { get; set; }

        [Required]
        public string AccountType { get; set; }

        [Required]
        public string IFSC { get; set; }

        [Required]
        public string BranchName { get; set; }

        [Required]
        public string BranchAddress { get; set; }

        [Required]
        public string BranchPhoneNo { get; set; }

        [Required]
        public string BranchEmailId { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public DateTime AccCreationDate { get; set; }
    }
}
