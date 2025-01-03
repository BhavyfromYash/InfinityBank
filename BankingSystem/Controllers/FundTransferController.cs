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

        public FundTransferController(IFundTransferService fundTransferService)
        {
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

        [HttpGet("{id}")]
        public async Task<IActionResult> ViewBeneficiariesAsync(int id)
        {
            var result = await _fundTransferService.GetBeneficiaryAsync(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeneficiaryAsync(int id)
        {
            var result = await _fundTransferService.DeleteBeneficiaryAsync(id);
            return Ok(result);
        }
    }
}
