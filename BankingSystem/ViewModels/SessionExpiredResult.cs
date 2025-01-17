using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.ViewModels
{
    public class SessionExpiredResult
    {
        public DateTime LastLogin { get; set; }
        public DateTime SessionExpired { get; set; }
        public string Suggestion { get; set; }
    }
}
