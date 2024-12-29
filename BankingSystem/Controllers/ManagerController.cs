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

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateManager([FromBody] Manager manager)
        {
            var result = await _managerService.CreateManagerAsync(manager);
            return Ok(result);
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
