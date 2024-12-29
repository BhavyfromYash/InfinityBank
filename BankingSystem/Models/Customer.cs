using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CusId { get; set; }
        public string Title { get; set; }
        public string Fname { get; set; }
        public string Mname { get; set; }
        public string Lname { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string AadhaarNo { get; set; }
        public string PanCardNo { get; set; }
        public DateTime DOB { get; set; }
        public string OccupationType { get; set; }
        public int SourceOfIncome { get; set; }
        public int GrossAnnualIncome { get; set; }
        public bool DebitCard { get; set; }
        public bool NetBanking { get; set; }
        public string Status { get; set; }
    }
}
