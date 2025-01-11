using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundTransferController : ControllerBase
    {
        private readonly IFundTransferService _fundTransferService;
        private readonly ILogger<FundTransferController> _logger;

        public FundTransferController(
            IFundTransferService fundTransferService,
            ILogger<FundTransferController> logger
        )
        {
            _logger = logger;
            _fundTransferService = fundTransferService;
        }

        [HttpPost("create-beneficiary")]
        public async Task<IActionResult> CreateBeneficiaryAsync(
            [FromBody] Beneficiary addBeneficiary
        )
        {
            var result = await _fundTransferService.AddBeneficiaryAsync(addBeneficiary);
            return Ok(result);
        }

        // [HttpGet("{id}")]
        // public async Task<IActionResult> ViewBeneficiariesAsync(int id)
        // {
        //     var result = await _fundTransferService.GetBeneficiaryByIdAsync(id);
        //     return Ok(result);
        // }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeneficiaryAsync(int id)
        {
            var result = await _fundTransferService.DeleteBeneficiaryAsync(id);
            return Ok(result);
        }

        [HttpGet("Show-Balance")]
        public async Task<IActionResult> ShowBalanceAsync()
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
            var account = await _fundTransferService.ShowAccountBalanceAsync(userId);
            if (account == null)
            {
                return NotFound("Account not found.");
            }
            return Ok(account);
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> TransferFundsAsync(
            [FromBody] CreateBeneficiaryTransaction createBeneficiaryTransaction
        )
        {
            if (ModelState.IsValid)
            { // Retrieve Beneficiary by BenId
                var beneficiary = await _fundTransferService.GetBeneficiaryByIdAsync(
                    createBeneficiaryTransaction.BenId
                );
                if (beneficiary == null)
                {
                    return NotFound(new { Message = "Beneficiary not found." });
                }
                var fundTransferBeneficiary = new FundTransferBeneficiary
                {
                    AccountNumber = createBeneficiaryTransaction.AccountNumber,
                    BenId = beneficiary.BenId, // Retrieve and assign the correct BenId
                    ConfirmAccountNumber = createBeneficiaryTransaction.AccountNumber,
                    AccountType = "Other", // Example logic
                    IFSC = createBeneficiaryTransaction.IFSC,
                    BankName = createBeneficiaryTransaction.BankName,
                    BranchName = createBeneficiaryTransaction.BranchName,
                    City = createBeneficiaryTransaction.City, // Ensure this is populated correctly
                };
                await _fundTransferService.TransferFundsAsync(
                    fundTransferBeneficiary,
                    createBeneficiaryTransaction.BenTransaction.Amount,
                    createBeneficiaryTransaction.BenTransaction.Remarks
                );
                return Ok(new { Message = "Funds transferred successfully." });
            }
            return BadRequest(
                new { Message = "Invalid fund transfer details.", Errors = ModelState.Values }
            );
        }

        // [HttpPost("transfer-within-bank")]
        // public async Task<IActionResult> TransferFundsWithinBankAsync(
        //     [FromBody] WithinBankViewModel withinBankViewModel,
        //     [FromQuery] string transMode
        // )
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(
        //             new { Message = "Invalid fund transfer details.", Errors = ModelState.Values }
        //         );
        //     }

        //     var beneficiary = await _fundTransferService.GetBeneficiaryByIdAsync(
        //         int.Parse(withinBankViewModel.BenId)
        //     );
        //     if (beneficiary == null)
        //     {
        //         return NotFound(new { Message = "Beneficiary not found." });
        //     }

        //     var withinBankBeneficiary = new WithinBankBeneficiary
        //     {
        //         AccountNumber = withinBankViewModel.AccountNumber,
        //         BenId = int.Parse(withinBankViewModel.BenId),
        //     };

        //     await _fundTransferService.TransferFundsWithinBankAsync(
        //         withinBankBeneficiary,
        //         withinBankViewModel.BenTransaction.Amount,
        //         withinBankViewModel.BenTransaction.Remarks,
        //         transMode
        //     ); // Use transMode from query parameter
        //     return Ok(new { Message = "Funds transferred within bank successfully." });
        // }

        [HttpPost("transfer-within-bank")]
        public async Task<IActionResult> TransferFundsWithinBankAsync(
            [FromBody] WithinBankViewModel withinBankViewModel,
            [FromQuery] string transMode
        )
        {
            _logger.LogInformation(
                "Starting TransferFundsWithinBankAsync in controller for BenId: {BenId}, TransMode: {TransMode}",
                withinBankViewModel.BenId,
                transMode
            );

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid fund transfer details: {ModelState}", ModelState);
                return BadRequest(
                    new { Message = "Invalid fund transfer details.", Errors = ModelState.Values }
                );
            }

            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                _logger.LogWarning("User is not logged in.");
                return Unauthorized(new { Message = "User is not logged in." });
            }

            if (!int.TryParse(userIdString, out int senderUserId))
            {
                _logger.LogError(
                    "Failed to parse UserId from session. SessionId: {SessionId}",
                    HttpContext.Session.Id
                );
                return Unauthorized(new { Message = "Invalid user session data." });
            }

            _logger.LogInformation(
                "Retrieving beneficiary with ID: {BenId}",
                withinBankViewModel.BenId
            );

            var beneficiary = await _fundTransferService.GetBeneficiaryByIdAsync(
                int.Parse(withinBankViewModel.BenId)
            );
            if (beneficiary == null)
            {
                _logger.LogWarning(
                    "Beneficiary not found with ID: {BenId}",
                    withinBankViewModel.BenId
                );
                return NotFound(new { Message = "Beneficiary not found." });
            }

            var withinBankBeneficiary = new WithinBankBeneficiary
            {
                AccountNumber = withinBankViewModel.AccountNumber,
                BenId = int.Parse(withinBankViewModel.BenId),
                Beneficiary = beneficiary, // Ensure beneficiary is assigned correctly
            };

            _logger.LogInformation("Beneficiary assigned to WithinBankBeneficiary");

            await _fundTransferService.TransferFundsWithinBankAsync(
                senderUserId,
                withinBankBeneficiary,
                withinBankViewModel.BenTransaction.Amount,
                withinBankViewModel.BenTransaction.Remarks,
                transMode
            );

            _logger.LogInformation("Funds transferred within bank successfully.");
            return Ok(new { Message = "Funds transferred within bank successfully." });
        }
    }
}
