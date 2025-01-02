// using System.Threading.Tasks;
// using BankingSystem.Models;
// using BankingSystem.Services;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;

// namespace BankingSystem.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class AccountController : ControllerBase
//     {
//         private readonly IAccountService _accountService;
//         private readonly ICustomerService _customerService; // Add CustomerService to fetch customer data
//         private readonly ILogger<AccountController> _logger;

//         public AccountController(
//             IAccountService accountService,
//             ICustomerService customerService,
//             ILogger<AccountController> logger
//         )
//         {
//             _accountService = accountService;
//             _customerService = customerService;
//             _logger = logger;
//         }

//         [HttpPost("create")]
//         public async Task<IActionResult> CreateAccount([FromBody] AccountCreationModel model)
//         {
//             if (model == null || !ModelState.IsValid)
//             {
//                 return BadRequest(ModelState);
//             }

//             var userIdString = HttpContext.Session.GetString("UserId");
//             if (string.IsNullOrEmpty(userIdString))
//             {
//                 return Unauthorized("User is not logged in.");
//             }

//             if (!int.TryParse(userIdString, out int userId))
//             {
//                 _logger.LogError(
//                     "Failed to parse UserId from session. SessionId: {SessionId}",
//                     HttpContext.Session.Id
//                 );
//                 return Unauthorized("Invalid user session data.");
//             }

//             // Fetch customer data
//             var customer = await _customerService.GetCustomerByIdAsync(model.NewAccount.CusId);
//             if (customer == null || customer.Status != "Approved")
//             {
//                 return BadRequest("Customer is not approved or does not exist.");
//             }

//             var newAccount = new Account
//             {
//                 HolderName = model.NewAccount.HolderName,
//                 AccountNumber = model.NewAccount.AccountNumber,
//                 CusId = customer.CusId, // Ensure the account is linked to the approved customer
//                 AccountType = model.NewAccount.AccountType,
//                 IFSC = model.NewAccount.IFSC,
//                 BranchName = model.NewAccount.BranchName,
//                 ToDate = model.NewAccount.ToDate,
//                 FromDate = model.NewAccount.FromDate,
//                 BranchPhoneNo = model.NewAccount.BranchPhoneNo,
//                 BranchEmailId = model.NewAccount.BranchEmailId,
//                 Balance = model.NewAccount.Balance,
//                 AccCreationDate = DateTime.UtcNow,
//                 UserId = userId,
//             };

//             var result = await _accountService.CreateAccountAsync(newAccount);
//             return Ok(result);
//         }

//         [HttpGet("{accountId}")]
//         public async Task<IActionResult> GetAccountById(int accountId)
//         {
//             var account = await _accountService.GetAccountByIdAsync(accountId);
//             if (account == null)
//             {
//                 return NotFound("Account not found.");
//             }
//             return Ok(account);
//         }

// [HttpGet("view-account-statement")]
// public async Task<IActionResult> ViewAccountStatement()
// {
//     var userIdString = HttpContext.Session.GetString("UserId");
//     if (string.IsNullOrEmpty(userIdString))
//     {
//         return Unauthorized("User is not logged in.");
//     }
//     if (!int.TryParse(userIdString, out int userId))
//     {
//         _logger.LogError(
//             "Failed to parse UserId from session. SessionId: {SessionId}",
//             HttpContext.Session.Id
//         );
//         return Unauthorized("Invalid user session data.");
//     }
//     var accountStatement = await _accountService.ViewAccountStatementAsync(userId);
//     if (accountStatement == null)
//     {
//         return NotFound("Account statement not found.");
//     }
//     return Ok(accountStatement);
// }
//     }
// }


using System.Threading.Tasks;
using BankingSystem.Models;
using BankingSystem.Services;
using BankingSystem.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountCreationModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("User is not logged in.");
            }

            if (!int.TryParse(userIdString, out int userId))
            {
                _logger.LogError(
                    "Failed to parse UserId from session. SessionId: {SessionId}",
                    HttpContext.Session.Id
                );
                return Unauthorized("Invalid user session data.");
            }

            var newAccount = new Account
            {
                HolderName = model.NewAccount.HolderName,
                AccountNumber = model.NewAccount.AccountNumber,
                CusId = model.NewAccount.CusId,
                AccountType = model.NewAccount.AccountType,
                IFSC = model.NewAccount.IFSC,
                BranchName = model.NewAccount.BranchName,
                ToDate = model.NewAccount.ToDate,
                FromDate = model.NewAccount.FromDate,
                BranchPhoneNo = model.NewAccount.BranchPhoneNo,
                BranchEmailId = model.NewAccount.BranchEmailId,
                Balance = model.NewAccount.Balance,
                AccCreationDate = DateTime.UtcNow,
                UserId = userId,
            };

            var result = await _accountService.CreateAccountAsync(newAccount);
            return Ok(result);
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionViewModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _accountService.DepositAsync(model);
            if (!result)
            {
                return BadRequest("Deposit failed.");
            }

            return Ok("Deposit successful.");
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionViewModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("User is not logged in.");
            }
            if (!int.TryParse(userIdString, out int userId))
            {
                _logger.LogError(
                    "Failed to parse UserId from session. SessionId: {SessionId}",
                    HttpContext.Session.Id
                );
                return Unauthorized("Invalid user session data.");
            }
            var account = await _accountService.GetAccountByUserIdAsync(userId);
            if (account == null)
            {
                return BadRequest("Account not found.");
            }
            model.AccountId = account.AccountId; // Ensure the user can only withdraw from their own account
            var result = await _accountService.WithdrawAsync(model);
            if (!result)
            {
                return BadRequest("Withdrawal failed.");
            }
            return Ok("Withdrawal successful.");
        }

        [HttpGet("view-account-statement")]
        public async Task<IActionResult> ViewAccountStatement()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("User is not logged in.");
            }
            if (!int.TryParse(userIdString, out int userId))
            {
                _logger.LogError(
                    "Failed to parse UserId from session. SessionId: {SessionId}",
                    HttpContext.Session.Id
                );
                return Unauthorized("Invalid user session data.");
            }
            var account = await _accountService.GetAccountByUserIdAsync(userId);
            if (account == null)
            {
                return NotFound("Account statement not found.");
            }
            var accountStatement = await _accountService.ViewAccountStatementAsync(userId);
            if (accountStatement == null)
            {
                return NotFound("Account statement not found.");
            }
            return Ok(accountStatement);
        }
    }
}
