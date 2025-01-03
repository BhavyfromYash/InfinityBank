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
                BranchAddress = model.NewAccount.BranchAddress,
                BranchEmailId = model.NewAccount.BranchEmailId,
                Balance = model.NewAccount.Balance,
                AccCreationDate = DateTime.UtcNow.Date,
                UserId = userId,
            };

            try
            {
                var result = await _accountService.CreateAccountAsync(newAccount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating account for user ID: {UserId}", userId);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Internal server error"
                );
            }
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
            var (isSuccess, message) = await _accountService.WithdrawAsync(model, userId);
            if (!isSuccess)
            {
                return BadRequest(message);
            }
            _logger.LogInformation("Withdrawal successful for user ID: {UserId}", userId);
            return Ok(message);
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
            var accountStatement = await _accountService.ViewAccountStatementAsync(userId);
            if (accountStatement == null)
            {
                _logger.LogWarning("Account statement not found for user ID: {UserId}", userId);
                return NotFound("Account statement not found.");
            }
            _logger.LogInformation("Account statement retrieved for user ID: {UserId}", userId);
            return Ok(accountStatement);
        }

        [HttpGet("account-summary")]
        public async Task<IActionResult> GetAccountSummary()
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
            var accountSummary = await _accountService.GetAccountSummaryAsync(userId);
            if (accountSummary == null)
            {
                return NotFound("Account not found.");
            }
            return Ok(accountSummary);
        }

        [HttpGet("account-details")]
        public async Task<IActionResult> GetAccountDetails()
        { // Retrieve UserId from session
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("User is not logged in.");
            }
            if (!int.TryParse(userIdString, out int userId))
            {
                return Unauthorized("Invalid user session data.");
            }
            var details = await _accountService.GetAccountDetailsAsync(userId);
            return Ok(details);
        }
    }
}
