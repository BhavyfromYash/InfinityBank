using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Repository
{
    public class UserRepository : IUserService
    {
        private readonly BankDbContext _context;

        public UserRepository(BankDbContext context)
        {
            _context = context;
        }

        public async Task<User> UserRegisterAsync(User newUser)
        {
            var user = await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return user.Entity;
        }

        public async Task<User> UserLoginAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            // Check if user exists and password is correct
            return user != null && user.Password == password ? user : null;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> ViewProfileAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> UpdateProfileAsync(int userId, User updatedUser)
        {
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null)
            {
                throw new InvalidOperationException("User not found");
            }

            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;
            existingUser.Password = updatedUser.Password;
            existingUser.ConfirmPassword = updatedUser.ConfirmPassword;
            existingUser.UserRole = updatedUser.UserRole;
            existingUser.OTP = updatedUser.OTP;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User> LogOutUserAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> UpdatePasswordAsync(
            string email,
            string newPassword,
            string confirmPassword
        )
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                user.Password = newPassword;
                user.ConfirmPassword = confirmPassword;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> SaveLogoutDetailsAsync(LogOut logOut)
        {
            await _context.LogOuts.AddAsync(logOut);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserAccountStatus> GetUserAccountStatusByUserIdAsync(int userId)
        {
            return await _context.UserAccountStatus.FirstOrDefaultAsync(s => s.UserId == userId);
        }

        public async Task UpdateUserAccountStatusAsync(UserAccountStatus status)
        {
            _context.UserAccountStatus.Update(status);
            await _context.SaveChangesAsync();
        }
    }
}

// using System.Threading.Tasks;

// namespace BankingSystem.Repository
// {
//     public class UserRepository : IUserService
//     {
//         private readonly BankDbContext _context;

//         public UserRepository(BankDbContext context)
//         {
//             _context = context;
//         }

//         public async Task<User> UserRegisterAsync(User newUser)
//         {
//             var user = await _context.Users.AddAsync(newUser);
//             await _context.SaveChangesAsync();
//             return user.Entity;
//         }

//         public async Task<User> UserLoginAsync(string email, string password)
//         {
//             var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

//             // Check if user exists
//             if (user == null)
//             {
//                 return null;
//             }

//             // Check account status
//             var accountStatus = await _context.UserAccountStatus.FirstOrDefaultAsync(s =>
//                 s.UserId == user.UserId
//             );
//             if (accountStatus == null)
//             {
//                 accountStatus = new UserAccountStatus
//                 {
//                     UserId = user.UserId,
//                     FailedLoginAttempts = 0,
//                     IsLocked = false,
//                 };
//                 await _context.UserAccountStatus.AddAsync(accountStatus);
//                 await _context.SaveChangesAsync();
//             }

//             if (accountStatus.IsLocked)
//             {
//                 return null;
//             }

//             // Check if password is correct
//             if (user.Password == password)
//             {
//                 accountStatus.FailedLoginAttempts = 0;
//                 await UpdateUserAccountStatusAsync(accountStatus);
//                 return user;
//             }
//             else
//             {
//                 accountStatus.FailedLoginAttempts++;
//                 if (accountStatus.FailedLoginAttempts >= 3)
//                 {
//                     accountStatus.IsLocked = true;
//                 }
//                 await UpdateUserAccountStatusAsync(accountStatus);
//                 return null;
//             }
//         }

//         public async Task<User> GetUserByIdAsync(int userId)
//         {
//             return await _context.Users.FindAsync(userId);
//         }

//         public async Task<User> ViewProfileAsync(int userId)
//         {
//             return await _context.Users.FindAsync(userId);
//         }

//         public async Task<User> UpdateProfileAsync(int userId, User updatedUser)
//         {
//             var existingUser = await _context.Users.FindAsync(userId);
//             if (existingUser == null)
//             {
//                 throw new InvalidOperationException("User not found");
//             }

//             existingUser.Name = updatedUser.Name;
//             existingUser.Email = updatedUser.Email;
//             existingUser.Password = updatedUser.Password;
//             existingUser.ConfirmPassword = updatedUser.ConfirmPassword;
//             existingUser.UserRole = updatedUser.UserRole;
//             existingUser.OTP = updatedUser.OTP;

//             _context.Users.Update(existingUser);
//             await _context.SaveChangesAsync();
//             return existingUser;
//         }

//         public async Task<bool> UserExistsAsync(string email)
//         {
//             return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
//         }

//         public async Task<User> LogOutUserAsync(int userId)
//         {
//             return await _context.Users.FindAsync(userId);
//         }

//         public async Task<User> GetUserByEmailAsync(string email)
//         {
//             return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
//         }

//         public async Task<bool> UpdatePasswordAsync(
//             string email,
//             string newPassword,
//             string confirmPassword
//         )
//         {
//             var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
//             if (user != null)
//             {
//                 user.Password = newPassword;
//                 user.ConfirmPassword = confirmPassword;
//                 await _context.SaveChangesAsync();
//                 return true;
//             }
//             return false;
//         }

//         public async Task<bool> SaveLogoutDetailsAsync(LogOut logOut)
//         {
//             await _context.LogOuts.AddAsync(logOut);
//             await _context.SaveChangesAsync();
//             return true;
//         }

//         public async Task<UserAccountStatus> GetUserAccountStatusByUserIdAsync(int userId)
//         {
//             return await _context.UserAccountStatus.FirstOrDefaultAsync(s => s.UserId == userId);
//         }

//         public async Task UpdateUserAccountStatusAsync(UserAccountStatus status)
//         {
//             _context.UserAccountStatus.Update(status);
//             await _context.SaveChangesAsync();
//         }
//     }
// }
