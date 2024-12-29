using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class NetUser
    {
        [Key]
        public int NetId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int CusId { get; set; }
        public DateTime LastLoginTime { get; set; }
        public int LoginAttempts { get; set; }
        public string AccountLockStatus { get; set; }
    }
}
