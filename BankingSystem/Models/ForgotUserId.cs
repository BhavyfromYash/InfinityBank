using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class ForgotUserId
    {
        [Key]
        public int FUserId { get; set; }
        public int AccountNo { get; set; }
        public int OTP { get; set; }
    }
}
