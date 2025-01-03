using System.Threading.Tasks;
using BankingSystem.Models;
using BankingSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(
            ICustomerService customerService,
            ILogger<CustomerController> logger
        )
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpPost("create-customer")]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer newCustomer)
        {
            if (newCustomer == null)
            {
                return BadRequest("Customer data is required.");
            }

            // Retrieve UserId from session as string
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                _logger.LogWarning(
                    "UserId not found in session. SessionId: {SessionId}",
                    HttpContext.Session.Id
                );
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

            _logger.LogInformation(
                "UserId from session: {UserId}, SessionId: {SessionId}",
                userId,
                HttpContext.Session.Id
            );

            newCustomer.CusId = 0;
            var createdCustomer = await _customerService.CreateCustomerAsync(newCustomer);

            return CreatedAtAction(
                nameof(CreateCustomer),
                new { cusId = createdCustomer.CusId },
                createdCustomer
            );
        }

        // [HttpGet("customer/{cusId}")]
        // public async Task<IActionResult> GetCustomerById(int cusId)
        // {
        //     var customer = await _customerService.GetCustomerByIdAsync(cusId);
        //     if (customer == null)
        //     {
        //         return NotFound("Customer not found.");
        //     }
        //     return Ok(customer);
        // }
    }
}
