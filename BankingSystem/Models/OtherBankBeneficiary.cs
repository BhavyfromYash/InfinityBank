using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Models
{
    public class OtherBankBeneficiary
    {
       [ForeignKey("BenId")]
        public Beneficiary Beneficiary { get; set; }

        [Key]
        public int OtherBankBenId { get; set; }
        public string AccountNumber { get; set; }
        public string ConfirmAccountNumber {get;set;}
        public string AccountType {get;set;}
        public string IFSC {get;set;}
        public int BenId { get; set; }
    }
}
