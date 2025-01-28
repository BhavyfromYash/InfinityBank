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

        // [HttpPost("create")]
        // public async Task<IActionResult> CreateAccount([FromBody] AccountCreationModel model)
        // {
        //     if (model == null || !ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

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

        //     var newAccount = new Account
        //     {
        //         HolderName = model.NewAccount.HolderName,
        //         AccountNumber = model.NewAccount.AccountNumber,
        //         CusId = model.NewAccount.CusId,
        //         AccountType = model.NewAccount.AccountType,
        //         IFSC = model.NewAccount.IFSC,
        //         BranchName = model.NewAccount.BranchName,
        //         BranchPhoneNo = model.NewAccount.BranchPhoneNo,
        //         BranchAddress = model.NewAccount.BranchAddress,
        //         BranchEmailId = model.NewAccount.BranchEmailId,
        //         Balance = model.NewAccount.Balance,
        //         AccCreationDate = DateTime.UtcNow.Date,
        //         UserId = userId,
        //     };

        //     try
        //     {
        //         var result = await _accountService.CreateAccountAsync(newAccount);
        //         return Ok(result);
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Error creating account for user ID: {UserId}", userId);
        //         return StatusCode(
        //             StatusCodes.Status500InternalServerError,
        //             "Internal server error"
        //         );
        //     }
        // }

        [HttpPost("CreateAccount/{userId}")]
        public async Task<IActionResult> CreateAccountByIdAsync(int userId, [FromBody] NewAccountModel newAccount)
        {
            try
            {
                var result = await _accountService.CreateAccountByIdAsync(userId, newAccount);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("is-user-exists")]
        public async Task<IActionResult> IsUserExists([FromQuery] int userId)
        {
            var userExists = await _accountService.IsUserExistsAsync(userId);
            if (userExists)
            {
                return Ok(true);
            }
            return NotFound($"UserId {userId} is not exist");
        }

        // [HttpPost("deposit")]
        // public async Task<IActionResult> Deposit([FromBody] TransactionViewModel model)
        // {
        //     if (model == null || !ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     var result = await _accountService.DepositByIdAsync(model);
        //     if (!result)
        //     {
        //         return BadRequest("Deposit failed.");
        //     }

        //     return Ok("Deposit successful.");
        // }

        // [HttpPost("withdraw")]
        // public async Task<IActionResult> Withdraw([FromBody] TransactionViewModel model)
        // {
        //     if (model == null || !ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }
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
        //     var (isSuccess, message) = await _accountService.WithdrawByIdAsync(model, userId);
        //     if (!isSuccess)
        //     {
        //         return BadRequest(message);
        //     }
        //     _logger.LogInformation("Withdrawal successful for user ID: {UserId}", userId);
        //     return Ok(message);
        // }

        // [HttpPost("deposit/{accountId}")]
        // public async Task<IActionResult> Deposit([FromBody] TransactionViewModel model)
        // {
        //     if (model == null || !ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     var result = await _accountService.DepositByIdAsync(model);
        //     if (!result)
        //     {
        //         return BadRequest("Deposit failed.");
        //     }

        //     return Ok("Deposit successful.");
        // }

        // [HttpPost("withdraw/{accountId}")]
        // public async Task<IActionResult> Withdraw([FromBody] TransactionViewModel model)
        // {
        //     if (model == null || !ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     var (isSuccess, message) = await _accountService.WithdrawByIdAsync(model);
        //     if (!isSuccess)
        //     {
        //         return BadRequest(message);
        //     }

        //     return Ok("Withdrawal successful.");
        // }

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
        //         _logger.LogWarning("Account statement not found for user ID: {UserId}", userId);
        //         return NotFound("Account statement not found.");
        //     }
        //     _logger.LogInformation("Account statement retrieved for user ID: {UserId}", userId);
        //     return Ok(accountStatement);
        // }

        // [HttpGet("view-account-statement")]
        // public async Task<IActionResult> ViewAccountStatement(int userId)
        // {
        //     var userIdString = HttpContext.Session.GetString("UserId");
        //     if (string.IsNullOrEmpty(userIdString))
        //     {
        //         return Unauthorized("User is not logged in.");
        //     }
        //     if (!int.TryParse(userIdString, out int userId))
        //     {
        //         _logger.LogError("Failed to parse UserId from session. SessionId: {SessionId}", HttpContext.Session.Id);
        //         return Unauthorized("Invalid user session data.");
        //     }
        //     var accountStatement = await _accountService.ViewAccountStatementByIdAsync(userId);
        //     if (accountStatement == null)
        //     {
        //         _logger.LogWarning("Account statement not found for user ID: {UserId}", userId);
        //         return NotFound("Account statement not found.");
        //     }
        //     _logger.LogInformation("Account statement retrieved for user ID: {UserId}", userId);
        //     return Ok(accountStatement);
        // }

        // [HttpGet("view-account-statement")]
        // public async Task<IActionResult> ViewAccountStatement([FromQuery] int userId)
        // {
        //     var accountStatement = await _accountService.ViewAccountStatementByIdAsync(userId);

        //     if (accountStatement == null)
        //     {
        //         _logger.LogWarning("Account statement not found for user ID: {UserId}", userId);
        //         return NotFound("Account statement not found.");
        //     }

        //     _logger.LogInformation("Account statement retrieved for user ID: {UserId}", userId);
        //     return Ok(accountStatement);
        // }

        // [HttpGet("account-summary/{userId}")]
        // public async Task<IActionResult> GetAccountSummary([FromQuery] int userId)
        // {
        //     var accountSummary = await _accountService.GetAccountSummaryByIdAsync(userId);
        //     if (accountSummary == null)
        //     {
        //         _logger.LogWarning("Account summary not found for user ID: {UserId}", userId);
        //         return NotFound("Account not found.");
        //     }
        //     _logger.LogInformation("Account summary retrieved for user ID: {UserId}", userId);
        //     return Ok(accountSummary);
        // }

        [HttpGet("account-summary/{userId}")]
        public async Task<IActionResult> GetAccountSummary(int userId)
        {
            var accountSummary = await _accountService.GetAccountSummaryByIdAsync(userId);

            if (accountSummary == null)
            {
                _logger.LogWarning("Account summary not found for user ID: {UserId}", userId);
                return NotFound("Account not found.");
            }

            _logger.LogInformation("Account summary retrieved for user ID: {UserId}", userId);
            return Ok(accountSummary);
        }

        [HttpGet("view-account-statement/{userId}")]
        public async Task<IActionResult> ViewAccountStatement(int userId)
        {
            var accountStatement = await _accountService.ViewAccountStatementByIdAsync(userId);

            if (accountStatement == null)
            {
                _logger.LogWarning("Account statement not found for user ID: {UserId}", userId);
                return NotFound("Account statement not found.");
            }

            _logger.LogInformation("Account statement retrieved for user ID: {UserId}", userId);
            return Ok(accountStatement);
        }

        [HttpGet("view-account-statement/{userId}/by-date-range")]
        public async Task<IActionResult> ViewAccountStatementByDateRange(
            int userId,
            DateTime fromDate,
            DateTime toDate
        )
        {
            var accountStatement = await _accountService.ViewAccountStatementByDateRangeAsync(
                userId,
                fromDate,
                toDate
            );

            if (accountStatement == null)
            {
                _logger.LogWarning(
                    "Account statement not found for user ID: {UserId} within the specified date range",
                    userId
                );
                return NotFound("Account statement not found within the specified date range.");
            }

            _logger.LogInformation(
                "Account statement retrieved for user ID: {UserId} within the specified date range",
                userId
            );
            return Ok(accountStatement);
        }

        [HttpGet("account-details/{userId}")]
        public async Task<IActionResult> GetAccountDetails(int userId)
        {
            var details = await _accountService.GetAccountDetailsByIdAsync(userId);
            if (details == null)
            {
                return NotFound("Account details not found.");
            }
            return Ok(details);
        }

        [HttpGet("ShowBalance/{accountId}")]
        public async Task<IActionResult> GetAccountBalance(int accountId)
        {
            var account = await _accountService.GetAccountBalanceAsync(accountId);
            if (account == null)
            {
                return NotFound("Account Balance not found");
            }
            return Ok(account);
        }

    }
}
