using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Services
{
    public interface IUserService
    {
        Task<User> UserRegisterAsync(User newUser);
        Task<User> UserLoginAsync(string email, string password);
        Task<User> GetUserByIdAsync(int userId);
        Task<User> ViewProfileAsync(int userId);
        Task<User> UpdateProfileAsync(int userId, User updatedUser);
        Task<bool> UserExistsAsync(string email);
        Task<User> LogOutUserAsync(int userId);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> UpdatePasswordAsync(string email, string newPassword, string confirmPassword);
        Task<bool> SaveLogoutDetailsAsync(LogOut logOut);
        Task<UserAccountStatus> GetUserAccountStatusByUserIdAsync(int userId);
        Task UpdateUserAccountStatusAsync(UserAccountStatus status);
        Task<SessionExpiredResult> SessionExpired(); 

    }
}
