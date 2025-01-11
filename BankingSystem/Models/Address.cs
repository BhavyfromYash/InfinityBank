// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace BankingSystem.Models
// {
//     public class Address
//     {
//         [Key] 
//         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//         public int AddressId { get; set; }
//         public string AddressLine { get; set; }
//         public string Landmark { get; set; }
//         public string Pincode { get; set; }
//         public string City { get; set; }
//         public string State { get; set; }
//     }
// }

namespace BankingSystem.Models
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }
        public string AddressLine { get; set; }
        public string Landmark { get; set; }
        public string Pincode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}

