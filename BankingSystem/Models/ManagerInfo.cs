using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class ManagerInfo
    {
        [Key]
        public int Id { get; set; }
        public string MobileNo { get; set; }
        public string City { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
