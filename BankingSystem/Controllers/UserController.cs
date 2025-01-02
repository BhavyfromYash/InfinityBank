using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.Models;
using BankingSystem.Services;
using BankingSystem.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BankingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private IConfiguration config;

        public UserController(IUserService userService, IConfiguration _config)
        {
            config = _config;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest("User data is required.");
            }

            if (
                string.IsNullOrWhiteSpace(newUser.Email)
                || string.IsNullOrWhiteSpace(newUser.Password)
            )
            {
                return BadRequest("Email and Password are required.");
            }

            if (await _userService.UserExistsAsync(newUser.Email))
            {
                return Conflict("User with this email already exists.");
            }

            var registeredUser = await _userService.UserRegisterAsync(newUser);
            return CreatedAtAction(
                nameof(GetUserById),
                new { userId = registeredUser.UserId },
                registeredUser
            );
        }

        // [HttpPost("login")]
        // public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        // {
        //     if (model == null)
        //     {
        //         return BadRequest("User data is required.");
        //     }
        //     if (string.IsNullOrWhiteSpace(model.Email))
        //     {
        //         return BadRequest("Email is required.");
        //     }
        //     if (string.IsNullOrWhiteSpace(model.Password))
        //     {
        //         return BadRequest("Password is required.");
        //     }
        //     var user = await _userService.UserLoginAsync(model.Email, model.Password);
        //     if (user != null)
        //     {
        //         HttpContext.Session.SetString("UserId", user.UserId.ToString());
        //         HttpContext.Session.SetString("LastLogin", DateTime.UtcNow.ToString("o"));
        //         return Ok("Login successful.");
        //     }
        //     else
        //     {
        //         var userCheck = await _userService.GetUserByEmailAsync(model.Email);
        //         if (userCheck != null)
        //         {
        //             var accountStatus = await _userService.GetUserAccountStatusByUserIdAsync(
        //                 userCheck.UserId
        //             );
        //             if (accountStatus != null && accountStatus.IsLocked)
        //             {
        //                 return Unauthorized("Your account has been locked. Consult the bank.");
        //             }
        //         }
        //         return Unauthorized("Invalid email or password.");
        //     }
        // }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            if (model == null)
            {
                return BadRequest("User data is required.");
            }
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                return BadRequest("Email is required.");
            }
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest("Password is required.");
            }
            var user = await _userService.UserLoginAsync(model.Email, model.Password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("LastLogin", DateTime.UtcNow.ToString("o")); // Generate JWT token
                var token = GenerateToken(user);
                return Ok(
                    new
                    {
                        UserId = user.UserId,
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        ConfirmPassword = user.ConfirmPassword,
                        UserRole = user.UserRole,
                        OTP = user.OTP,
                        Token = token,
                    }
                );
            }
            else
            {
                var userCheck = await _userService.GetUserByEmailAsync(model.Email);
                if (userCheck != null)
                {
                    var accountStatus = await _userService.GetUserAccountStatusByUserIdAsync(
                        userCheck.UserId
                    );
                    if (accountStatus != null && accountStatus.IsLocked)
                    {
                        return Unauthorized("Your account has been locked. Consult the bank.");
                    }
                }
                return Unauthorized("Invalid email or password.");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateProfile(int userId, [FromBody] User updatedUser)
        {
            if (updatedUser == null)
            {
                return BadRequest("User data is required.");
            }

            try
            {
                var user = await _userService.UpdateProfileAsync(userId, updatedUser);
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("exists")]
        public async Task<IActionResult> UserExists(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email is required.");
            }

            var exists = await _userService.UserExistsAsync(email);
            return Ok(new { Exists = exists });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                return BadRequest("Email is required.");
            }

            var user = await _userService.GetUserByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound("Email not found.");
            }

            return Ok("Email exists. Proceed to reset password.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest model)
        {
            if (
                string.IsNullOrWhiteSpace(model.Email)
                || string.IsNullOrWhiteSpace(model.Password)
                || string.IsNullOrWhiteSpace(model.ConfirmPassword)
            )
            {
                return BadRequest("Email, new password, and confirm password are required.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("New password and confirm password do not match.");
            }

            var isPasswordUpdated = await _userService.UpdatePasswordAsync(
                model.Email,
                model.Password,
                model.ConfirmPassword
            );
            if (isPasswordUpdated)
            {
                return Ok("Password has been updated successfully.");
            }
            else
            {
                return NotFound("Email not found.");
            }
        }

        [HttpGet("logout/{userId}")]
        public async Task<IActionResult> LogOutUser(int userId)
        {
            var user = await _userService.LogOutUserAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            HttpContext.Session.Clear();
            return Ok(user);
        }

        [HttpGet("sessionexpired")]
        public async Task<IActionResult> SessionExpired()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return Unauthorized("Session is not active.");
            }

            var lastLoginStr = HttpContext.Session.GetString("LastLogin");
            DateTime lastLogin;

            if (
                !DateTime.TryParse(
                    lastLoginStr,
                    null,
                    System.Globalization.DateTimeStyles.RoundtripKind,
                    out lastLogin
                )
            )
            {
                return BadRequest("Invalid session data.");
            }

            var now = DateTime.UtcNow;
            if ((now - lastLogin).TotalMinutes < 3)
            {
                return Ok(
                    new
                    {
                        LastLogin = lastLogin,
                        SessionExpired = lastLogin.AddMinutes(3),
                        Suggestion = "Your session is still active.",
                    }
                );
            }

            var sessionExpired = lastLogin.AddMinutes(3);
            var suggestion = "Your session has been expired. Login again.";

            var logOut = new LogOut
            {
                LastLogin = lastLogin,
                SessionExpired = sessionExpired,
                Suggestion = suggestion,
            };

            await _userService.SaveLogoutDetailsAsync(logOut);

            HttpContext.Session.Clear();

            return Ok(
                new
                {
                    LastLogin = lastLogin,
                    SessionExpired = sessionExpired,
                    Suggestion = suggestion,
                }
            );
        }

        [NonAction]
        private string GenerateToken(User user)
        {
            //This is a claims list that defined who are the subject, what is to be checked and
            //who are the audience for the claim

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.UserRole),
            };

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config.GetValue<string>("Jwt:secret"))
            );
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: config.GetValue<string>("Jwt:issuer"),
                audience: config.GetValue<string>("Jwt:audience"),
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}

// using System.Collections.Generic;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using System.Threading.Tasks;
// using BankingSystem.Models;
// using BankingSystem.Services;
// using BankingSystem.ViewModels;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
// using Microsoft.IdentityModel.Tokens;

// namespace BankingSystem.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class UserController : ControllerBase
//     {
//         private readonly IUserService _userService;
//         private IConfiguration config;

//         public UserController(IUserService userService, IConfiguration _config)
//         {
//             config = _config;
//             _userService = userService;
//         }

//         [HttpPost("register")]
//         public async Task<IActionResult> Register([FromBody] User newUser)
//         {
//             if (newUser == null)
//             {
//                 return BadRequest("User data is required.");
//             }

//             if (
//                 string.IsNullOrWhiteSpace(newUser.Email)
//                 || string.IsNullOrWhiteSpace(newUser.Password)
//             )
//             {
//                 return BadRequest("Email and Password are required.");
//             }

//             if (await _userService.UserExistsAsync(newUser.Email))
//             {
//                 return Conflict("User with this email already exists.");
//             }

//             var registeredUser = await _userService.UserRegisterAsync(newUser);
//             return CreatedAtAction(
//                 nameof(GetUserById),
//                 new { userId = registeredUser.UserId },
//                 registeredUser
//             );
//         }

//         [HttpPost]
//         [Route("login")]
//         public async Task<IActionResult> Login([FromBody] UserLoginModel model)
//         {
//             if (model == null)
//             {
//                 return BadRequest("User data is required.");
//             }
//             if (string.IsNullOrWhiteSpace(model.Email))
//             {
//                 return BadRequest("Email is required.");
//             }
//             if (string.IsNullOrWhiteSpace(model.Password))
//             {
//                 return BadRequest("Password is required.");
//             }
//             var user = await _userService.UserLoginAsync(model.Email, model.Password);
//             if (user != null)
//             {
//                 HttpContext.Session.SetString("UserId", user.UserId.ToString());
//                 HttpContext.Session.SetString("LastActivity", DateTime.UtcNow.ToString("o")); // Set the last activity time
//                 var token = GenerateToken(user);
//                 return Ok(
//                     new
//                     {
//                         UserId = user.UserId,
//                         Name = user.Name,
//                         Email = user.Email,
//                         Password = user.Password,
//                         ConfirmPassword = user.ConfirmPassword,
//                         UserRole = user.UserRole,
//                         OTP = user.OTP,
//                         Token = token,
//                     }
//                 );
//             }
//             else
//             {
//                 var userCheck = await _userService.GetUserByEmailAsync(model.Email);
//                 if (userCheck != null)
//                 {
//                     var accountStatus = await _userService.GetUserAccountStatusByUserIdAsync(
//                         userCheck.UserId
//                     );
//                     if (accountStatus != null && accountStatus.IsLocked)
//                     {
//                         return Unauthorized("Your account has been locked. Consult the bank.");
//                     }
//                 }
//                 return Unauthorized("Invalid email or password.");
//             }
//         }

//         [HttpGet("{userId}")]
//         public async Task<IActionResult> GetUserById(int userId)
//         {
//             var user = await _userService.GetUserByIdAsync(userId);
//             if (user == null)
//             {
//                 return NotFound("User not found.");
//             }
//             UpdateLastActivity(); // Update last activity
//             return Ok(user);
//         }

//         [HttpPut("{userId}")]
//         public async Task<IActionResult> UpdateProfile(int userId, [FromBody] User updatedUser)
//         {
//             if (updatedUser == null)
//             {
//                 return BadRequest("User data is required.");
//             }

//             try
//             {
//                 var user = await _userService.UpdateProfileAsync(userId, updatedUser);
//                 UpdateLastActivity(); // Update last activity
//                 return Ok(user);
//             }
//             catch (InvalidOperationException ex)
//             {
//                 return NotFound(ex.Message);
//             }
//         }

//         [HttpGet("exists")]
//         public async Task<IActionResult> UserExists(string email)
//         {
//             if (string.IsNullOrWhiteSpace(email))
//             {
//                 return BadRequest("Email is required.");
//             }

//             var exists = await _userService.UserExistsAsync(email);
//             UpdateLastActivity(); // Update last activity
//             return Ok(new { Exists = exists });
//         }

//         [HttpPost("forgot-password")]
//         public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest model)
//         {
//             if (string.IsNullOrWhiteSpace(model.Email))
//             {
//                 return BadRequest("Email is required.");
//             }

//             var user = await _userService.GetUserByEmailAsync(model.Email);
//             if (user == null)
//             {
//                 return NotFound("Email not found.");
//             }

//             UpdateLastActivity(); // Update last activity
//             return Ok("Email exists. Proceed to reset password.");
//         }

//         [HttpPost("reset-password")]
//         public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest model)
//         {
//             if (
//                 string.IsNullOrWhiteSpace(model.Email)
//                 || string.IsNullOrWhiteSpace(model.Password)
//                 || string.IsNullOrWhiteSpace(model.ConfirmPassword)
//             )
//             {
//                 return BadRequest("Email, new password, and confirm password are required.");
//             }

//             if (model.Password != model.ConfirmPassword)
//             {
//                 return BadRequest("New password and confirm password do not match.");
//             }

//             var isPasswordUpdated = await _userService.UpdatePasswordAsync(
//                 model.Email,
//                 model.Password,
//                 model.ConfirmPassword
//             );
//             if (isPasswordUpdated)
//             {
//                 UpdateLastActivity(); // Update last activity
//                 return Ok("Password has been updated successfully.");
//             }
//             else
//             {
//                 return NotFound("Email not found.");
//             }
//         }

//         [HttpGet("logout/{userId}")]
//         public async Task<IActionResult> LogOutUser(int userId)
//         {
//             var user = await _userService.LogOutUserAsync(userId);
//             if (user == null)
//             {
//                 return NotFound("User not found.");
//             }

//             HttpContext.Session.Clear();
//             return Ok(user);
//         }

//         [HttpGet("sessionexpired")]
//         public IActionResult SessionExpired()
//         {
//             if (HttpContext.Session.GetString("UserId") == null)
//             {
//                 return Unauthorized("Session is not active.");
//             }

//             var lastActivityStr = HttpContext.Session.GetString("LastActivity");
//             DateTime lastActivity;

//             if (
//                 !DateTime.TryParse(
//                     lastActivityStr,
//                     null,
//                     System.Globalization.DateTimeStyles.RoundtripKind,
//                     out lastActivity
//                 )
//             )
//             {
//                 return BadRequest("Invalid session data.");
//             }

//             var now = DateTime.UtcNow;
//             if ((now - lastActivity).TotalMinutes < 3)
//             {
//                 return Ok(
//                     new
//                     {
//                         LastActivity = lastActivity,
//                         SessionExpired = lastActivity.AddMinutes(3),
//                         Suggestion = "Your session is still active.",
//                     }
//                 );
//             }

//             HttpContext.Session.Clear();

//             return Ok(
//                 new
//                 {
//                     LastActivity = lastActivity,
//                     SessionExpired = lastActivity.AddMinutes(3),
//                     Suggestion = "Your session has expired. Please log in again.",
//                 }
//             );
//         }

//         [NonAction]
//         private string GenerateToken(User user)
//         {
//             var claims = new List<Claim>
//             {
//                 new Claim(JwtRegisteredClaimNames.Sub, user.Name),
//                 new Claim(JwtRegisteredClaimNames.Email, user.Email),
//                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                 new Claim(ClaimTypes.Role, user.UserRole),
//             };

//             var securityKey = new SymmetricSecurityKey(
//                 Encoding.UTF8.GetBytes(config.GetValue<string>("Jwt:secret"))
//             );
//             var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
//             var token = new JwtSecurityToken(
//                 issuer: config.GetValue<string>("Jwt:issuer"),
//                 audience: config.GetValue<string>("Jwt:audience"),
//                 claims: claims,
//                 expires: DateTime.Now.AddMinutes(30),
//                 signingCredentials: credentials
//             );
//             string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
//             return tokenString;
//         }

//         [NonAction]
//         private void UpdateLastActivity()
//         {
//             HttpContext.Session.SetString("LastActivity", DateTime.UtcNow.ToString("o"));
//         }
//     }
// }
