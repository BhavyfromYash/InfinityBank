using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class UserAccountStatus
    {
        [Key]
        public int UserAccountStatusId { get; set; }

        [Required]
        public int UserId { get; set; }
        public int FailedLoginAttempts { get; set; }
        public bool IsLocked { get; set; }
    }
}
