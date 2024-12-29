using System.ComponentModel.DataAnnotations;

namespace BankingSystem.ViewModels
{
    public class UserLoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
