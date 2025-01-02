// using System.Collections.Generic;
// using System.Threading.Tasks;
// using BankingSystem.Models;
// using BankingSystem.Services;
// using Microsoft.AspNetCore.Mvc;

// namespace BankingSystem.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class ManagerController : ControllerBase
//     {
//         private readonly IManagerService _managerService;

//         public ManagerController(IManagerService managerService)
//         {
//             _managerService = managerService;
//         }

//         [HttpPost]
//         public async Task<IActionResult> CreateManager([FromBody] Manager manager)
//         {
//             var result = await _managerService.CreateManagerAsync(manager);
//             return Ok(result);
//         }

//         // [HttpGet("managers")]
//         // public async Task<IActionResult> GetAllManagers()
//         // {
//         //     var managers = await _managerService.GetAllManagersAsync();
//         //     return Ok(managers);
//         // }

//         [HttpGet("customers")]
//         [Authorize(Roles = "Manager")] // This will require the user to be in the "Manager" role to access this endpoint
//         public async Task<IActionResult> GetAllCustomers()
//         {
//             var customers = await _managerService.GetAllCustomersAsync();
//             return Ok(customers);
//         }

//         [HttpPost("approve-request/{cusId}")]
//         public async Task<IActionResult> ApproveCustomerRequest(int cusId)
//         {
//             var customer = await _managerService.ApproveCustomerRequestAsync(cusId);
//             if (customer == null)
//             {
//                 return NotFound("Customer request not found or already processed.");
//             }
//             return Ok(customer);
//         }

//         [HttpPost("reject-request/{cusId}")]
//         public async Task<IActionResult> RejectCustomerRequest(int cusId)
//         {
//             var customer = await _managerService.RejectCustomerRequestAsync(cusId);
//             if (customer == null)
//             {
//                 return NotFound("Customer request not found or already processed.");
//             }
//             return Ok(customer);
//         }

//         [HttpPost("unlock/{userId}")]
//         public async Task<IActionResult> UnlockUserAccount(int userId)
//         {
//             var user = await _managerService.UnlockUserAccountAsync(userId);
//             if (user == null)
//             {
//                 return NotFound("User not found.");
//             }
//             return Ok("User account has been unlocked.");
//         }
//     }
// }

using System.Collections.Generic;
using System.Threading.Tasks;
using BankingSystem.Models;
using BankingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ManagerController> _logger;

        public ManagerController(
            IManagerService managerService,
            IHttpContextAccessor httpContextAccessor,
            ILogger<ManagerController> logger
        )
        {
            _managerService = managerService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateManager([FromBody] ManagerCreationModel model)
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
            var newManager = new ManagerInfo
            {
                MobileNo = model.MobileNo,
                City = model.City,
                BranchName = model.BranchName,
                BranchAddress = model.BranchAddress,
                UserId = userId,
            };
            var result = await _managerService.CreateManagerAsync(newManager);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllManagers()
        {
            var managers = await _managerService.GetAllManagersAsync();
            return Ok(managers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetManagerById(int id)
        {
            var manager = await _managerService.GetManagerByIdAsync(id);
            if (manager == null)
            {
                return NotFound("Manager not found.");
            }
            return Ok(manager);
        }

        [HttpGet("profile")]
        public async Task<IActionResult> ViewProfile()
        {
            var userIdString = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return Unauthorized(new { Message = "User is not logged in or invalid session." });
            }
            var profile = await _managerService.ViewManagerProfileAsync(userId);
            if (profile == null)
            {
                return NotFound(new { Message = "Manager profile not found." });
            }
            return Ok(profile);
        }

        [HttpGet("customers")]
        [Authorize(Roles = "Manager")] // This will require the user to be in the "Manager" role to access this endpoint
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _managerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("pending-approvals")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetPendingApprovals()
        {
            var pendingApprovals = await _managerService.GetPendingApprovalsAsync();
            return Ok(pendingApprovals);
        }

        [HttpPost("approve-request/{cusId}")]
        public async Task<IActionResult> ApproveCustomerRequest(int cusId)
        {
            var customer = await _managerService.ApproveCustomerRequestAsync(cusId);
            if (customer == null)
            {
                return NotFound("Customer request not found or already processed.");
            }
            return Ok(customer);
        }

        [HttpPost("reject-request/{cusId}")]
        public async Task<IActionResult> RejectCustomerRequest(int cusId)
        {
            var customer = await _managerService.RejectCustomerRequestAsync(cusId);
            if (customer == null)
            {
                return NotFound("Customer request not found or already processed.");
            }
            return Ok(customer);
        }

        [HttpPost("unlock/{userId}")]
        public async Task<IActionResult> UnlockUserAccount(int userId)
        {
            var user = await _managerService.UnlockUserAccountAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok("User account has been unlocked.");
        }
    }
}
