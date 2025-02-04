using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.ViewModels
{
    public class CustomerCreationModel
    {
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

        public AddressViewModel Address { get; set; }
    }
}
