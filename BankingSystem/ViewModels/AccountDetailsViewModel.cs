using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.ViewModels
{
    public class AccountDetailsViewModel
    {
        [Key]
        public string AccountNumber { get; set; }

        [Required]
        public string HolderName { get; set; }

        [Required]
        public string CusId { get; set; }

        [Required]
        public string PanCardNo { get; set; }

        [Required]
        public string AadhaarNo { get; set; }

        [Required]
        public string EmailId { get; set; }

        [Required]
        public DateTime AccCreationDate { get; set; }

        [Required]
        public string AccountType { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public string BranchAddress { get; set; }

        [Required]
        public string IFSC { get; set; }

        [Required]
        public string BranchPhoneNo { get; set; }

        [Required]
        public string BranchEmailId { get; set; }
    }
}
