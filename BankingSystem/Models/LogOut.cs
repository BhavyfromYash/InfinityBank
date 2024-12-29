using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class LogOut
    {
        [Key]
        public int LogOutId { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime SessionExpired { get; set; }
        public string Suggestion { get; set; }
    }
}
