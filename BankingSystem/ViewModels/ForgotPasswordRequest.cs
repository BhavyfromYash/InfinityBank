using System.ComponentModel.DataAnnotations;

namespace BankingSystem.ViewModels
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
