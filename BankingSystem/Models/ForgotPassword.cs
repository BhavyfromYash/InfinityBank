using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class ForgotPassword
    {
        [Key]
        public int FPassId { get; set; }
        public int UserId { get; set; }
        public int OTP { get; set; }
    }
}
