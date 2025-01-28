using System.Collections.Generic;
using System.Threading.Tasks;
using BankingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Repository
{
    public class CustomerRepository : ICustomerService
    {
        private readonly BankDbContext _context;
        private readonly IManagerService _managerService;

        public CustomerRepository(BankDbContext context, IManagerService managerService)
        {
            _context = context;
            _managerService = managerService;
        }

        // public async Task<Customer> CreateCustomerAsync(Customer newCustomer)
        // {
        //     newCustomer.Status = "Pending"; // Ensure status is set to Pending
        //     _context.Addresses.Add(newCustomer.Address);
        //     await _context.SaveChangesAsync();
        //     newCustomer.Address = newCustomer.Address;
        //     _context.Customers.AddAsync(newCustomer);
        //     await _context.SaveChangesAsync();
        //     return newCustomer;
        // }

        // public async Task<Customer> CreateCustomerAsync(Customer customer)
        // { // Add Address first and save changes to generate AddressId
        //     customer.Status = "Pending";
        //     // Add Address first and save changes to generate AddressId
        //     _context.Addresses.Add(customer.Address);
        //     await _context.SaveChangesAsync(); // Now AddressId is set, so we can add the customer
        //     _context.Customers.Add(customer);
        //     await _context.SaveChangesAsync();
        //     return customer;
        // }

        //    public async Task<CustomerCreationModel> CreateCustomerDetailsByIdAsync(int userId, Customer newCustomerDetails)
        //     {
        //         if (!await IsUserExistsAsync(userId))
        //         {
        //             throw new ArgumentException($"User with ID {userId} does not exist.");
        //         }

        //         newCustomerDetails.Status = "Pending";

        //         _context.Addresses.Add(newCustomerDetails.Address);
        //         await _context.SaveChangesAsync();

        //         // Generate the CustomCusId
        //         var lastCustomer = await _context.Customers.OrderByDescending(c => c.CusId).FirstOrDefaultAsync();
        //         int lastIdNumber = lastCustomer != null ? int.Parse(lastCustomer.CusId.Substring(2)) : 500;
        //         newCustomerDetails.CusId = $"IB{(lastIdNumber + 2):D5}";

        //         _context.Customers.Add(newCustomerDetails);
        //         await _context.SaveChangesAsync();

        //         var customerModel = new CustomerCreationModel
        //         {
        //             Title = newCustomerDetails.Title,
        //             Fname = newCustomerDetails.Fname,
        //             Mname = newCustomerDetails.Mname,
        //             Lname = newCustomerDetails.Lname,
        //             MobileNo = newCustomerDetails.MobileNo,
        //             EmailId = newCustomerDetails.EmailId,
        //             AadhaarNo = newCustomerDetails.AadhaarNo,
        //             PanCardNo = newCustomerDetails.PanCardNo,
        //             DOB = newCustomerDetails.DOB,
        //             OccupationType = newCustomerDetails.OccupationType,
        //             SourceOfIncome = newCustomerDetails.SourceOfIncome,
        //             GrossAnnualIncome = newCustomerDetails.GrossAnnualIncome,
        //             DebitCard = newCustomerDetails.DebitCard,
        //             NetBanking = newCustomerDetails.NetBanking,
        //             Status = newCustomerDetails.Status,
        //             Address = newCustomerDetails.Address
        //         };

        //         return customerModel;
        //     }

        // public async Task<CustomerCreationModel> CreateCustomerDetailsByIdAsync(
        //     int userId,
        //     Customer newCustomerDetails
        // )
        // {
        //     if (!await IsUserExistsAsync(userId))
        //     {
        //         throw new ArgumentException($"User with ID {userId} does not exist.");
        //     }

        //     newCustomerDetails.Status = "Pending";

        //     // Generate the CustomCusId
        //     var lastCustomer = await _context
        //         .Customers.OrderByDescending(c => c.CusId)
        //         .FirstOrDefaultAsync();
        //     int lastIdNumber =
        //         lastCustomer != null ? int.Parse(lastCustomer.CusId.Substring(2)) : 500;
        //     newCustomerDetails.CusId = $"IB{(lastIdNumber + 2):D5}";

        //     // Ensure the Address CusId is set
        //     newCustomerDetails.Address.CusId = newCustomerDetails.CusId;

        //     _context.Customers.Add(newCustomerDetails);
        //     await _context.SaveChangesAsync();

        //     var customerModel = new CustomerCreationModel
        //     {
        //         Title = newCustomerDetails.Title,
        //         Fname = newCustomerDetails.Fname,
        //         Mname = newCustomerDetails.Mname,
        //         Lname = newCustomerDetails.Lname,
        //         MobileNo = newCustomerDetails.MobileNo,
        //         EmailId = newCustomerDetails.EmailId,
        //         AadhaarNo = newCustomerDetails.AadhaarNo,
        //         PanCardNo = newCustomerDetails.PanCardNo,
        //         DOB = newCustomerDetails.DOB,
        //         OccupationType = newCustomerDetails.OccupationType,
        //         SourceOfIncome = newCustomerDetails.SourceOfIncome,
        //         GrossAnnualIncome = newCustomerDetails.GrossAnnualIncome,
        //         DebitCard = newCustomerDetails.DebitCard,
        //         NetBanking = newCustomerDetails.NetBanking,
        //         Status = newCustomerDetails.Status,
        //         Address = new AddressViewModel
        //         {
        //             AddressLine = newCustomerDetails.Address.AddressLine,
        //             Landmark = newCustomerDetails.Address.Landmark,
        //             Pincode = newCustomerDetails.Address.Pincode,
        //             City = newCustomerDetails.Address.City,
        //             State = newCustomerDetails.Address.State,
        //         },
        //     };

        //     return customerModel;
        // }

        // public async Task<Customer> CreateCustomerDetailsByIdAsync(
        //     int userId,
        //     CustomerCreationModel newCustomerDetails
        // )
        // {
        //     // Check if user exists
        //     if (!await IsUserExistsAsync(userId))
        //     {
        //         throw new ArgumentException("User not found.");
        //     }

        //     var customerId = GenerateCustomerId();

        //     var customer = new Customer
        //     {
        //         CusId = customerId,
        //         Title = newCustomerDetails.Title,
        //         Fname = newCustomerDetails.Fname,
        //         Mname = newCustomerDetails.Mname,
        //         Lname = newCustomerDetails.Lname,
        //         MobileNo = newCustomerDetails.MobileNo,
        //         EmailId = newCustomerDetails.EmailId,
        //         AadhaarNo = newCustomerDetails.AadhaarNo,
        //         PanCardNo = newCustomerDetails.PanCardNo,
        //         DOB = newCustomerDetails.DOB,
        //         OccupationType = newCustomerDetails.OccupationType,
        //         SourceOfIncome = newCustomerDetails.SourceOfIncome,
        //         GrossAnnualIncome = newCustomerDetails.GrossAnnualIncome,
        //         DebitCard = newCustomerDetails.DebitCard,
        //         NetBanking = newCustomerDetails.NetBanking,
        //         Status = newCustomerDetails.Status,
        //         Address = new Address
        //         {
        //             AddressLine = newCustomerDetails.Address.AddressLine,
        //             Landmark = newCustomerDetails.Address.Landmark,
        //             Pincode = newCustomerDetails.Address.Pincode,
        //             City = newCustomerDetails.Address.City,
        //             State = newCustomerDetails.Address.State,
        //         },
        //     };

        //     _context.Customers.Add(customer);
        //     await _context.SaveChangesAsync();

        //     return customer;
        // }

        // private string GenerateCustomerId()
        // {
        //     var lastCustomer = _context.Customers.OrderByDescending(c => c.CusId).FirstOrDefault();

        //     if (lastCustomer == null)
        //     {
        //         return "IB00501";
        //     }

        //     var lastId = int.Parse(lastCustomer.CusId.Substring(4));
        //     var newId = lastId + 1;

        //     return "IB00" + newId.ToString("D3");
        // }
        // public async Task<Customer> CreateCustomerDetailsByIdAsync(
        //     int userId,
        //     CustomerCreationModel newCustomerDetails
        // )
        // {
        //     // Check if user exists
        //     if (!await IsUserExistsAsync(userId))
        //     {
        //         throw new ArgumentException("User not found.");
        //     }

        //     // Check if Aadhaar or PAN card already exists
        //     if (
        //         !await IsCustomerDetailsUniqueAsync(
        //             newCustomerDetails.AadhaarNo,
        //             newCustomerDetails.PanCardNo
        //         )
        //     )
        //     {
        //         throw new InvalidOperationException(
        //             "A customer with the same Aadhaar number or PAN card already exists."
        //         );
        //     }

        //     var customerId = GenerateCustomerId();

        //     var customer = new Customer
        //     {
        //         CusId = customerId,
        //         Title = newCustomerDetails.Title,
        //         Fname = newCustomerDetails.Fname,
        //         Mname = newCustomerDetails.Mname,
        //         Lname = newCustomerDetails.Lname,
        //         MobileNo = newCustomerDetails.MobileNo,
        //         EmailId = newCustomerDetails.EmailId,
        //         AadhaarNo = newCustomerDetails.AadhaarNo,
        //         PanCardNo = newCustomerDetails.PanCardNo,
        //         DOB = newCustomerDetails.DOB,
        //         OccupationType = newCustomerDetails.OccupationType,
        //         SourceOfIncome = newCustomerDetails.SourceOfIncome,
        //         GrossAnnualIncome = newCustomerDetails.GrossAnnualIncome,
        //         DebitCard = newCustomerDetails.DebitCard,
        //         NetBanking = newCustomerDetails.NetBanking,
        //         Status = "Pending", // Set status to "Pending" by default
        //         Address = new Address
        //         {
        //             AddressLine = newCustomerDetails.Address.AddressLine,
        //             Landmark = newCustomerDetails.Address.Landmark,
        //             Pincode = newCustomerDetails.Address.Pincode,
        //             City = newCustomerDetails.Address.City,
        //             State = newCustomerDetails.Address.State,
        //         },
        //     };

        //     _context.Customers.Add(customer);
        //     await _context.SaveChangesAsync();

        //     return customer;
        // }

        public async Task<Customer> CreateCustomerDetailsByIdAsync(
            int userId,
            CustomerCreationModel newCustomerDetails
        )
        {
            // Check if user exists
            if (!await IsUserExistsAsync(userId))
            {
                throw new ArgumentException("User not found.");
            }

            // Check if Aadhaar or PAN card already exists
            if (
                !await IsCustomerDetailsUniqueAsync(
                    newCustomerDetails.AadhaarNo,
                    newCustomerDetails.PanCardNo
                )
            )
            {
                throw new InvalidOperationException(
                    "A customer with the same Aadhaar number or PAN card already exists."
                );
            }

            var customerId = GenerateCustomerId();
            var referenceNumber = GenerateReferenceNumber(); // Generate a random reference number

            var customer = new Customer
            {
                CusId = customerId,
                Title = newCustomerDetails.Title,
                Fname = newCustomerDetails.Fname,
                Mname = newCustomerDetails.Mname,
                Lname = newCustomerDetails.Lname,
                MobileNo = newCustomerDetails.MobileNo,
                EmailId = newCustomerDetails.EmailId,
                AadhaarNo = newCustomerDetails.AadhaarNo,
                PanCardNo = newCustomerDetails.PanCardNo,
                DOB = newCustomerDetails.DOB,
                OccupationType = newCustomerDetails.OccupationType,
                SourceOfIncome = newCustomerDetails.SourceOfIncome,
                GrossAnnualIncome = newCustomerDetails.GrossAnnualIncome,
                DebitCard = newCustomerDetails.DebitCard,
                NetBanking = newCustomerDetails.NetBanking,
                Status = "Pending", // Set status to "Pending" by default
                Address = new Address
                {
                    AddressLine = newCustomerDetails.Address.AddressLine,
                    Landmark = newCustomerDetails.Address.Landmark,
                    Pincode = newCustomerDetails.Address.Pincode,
                    City = newCustomerDetails.Address.City,
                    State = newCustomerDetails.Address.State,
                },
                ReferenceNumber = referenceNumber, // Store the generated reference number
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            // Send reference number for approval
           // await _managerService.SendReferenceNumberForApproval(referenceNumber, customer.CusId);

            return customer;
        }

        private string GenerateReferenceNumber()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
        }

        private string GenerateCustomerId()
        {
            var lastCustomer = _context.Customers.OrderByDescending(c => c.CusId).FirstOrDefault();

            if (lastCustomer == null)
            {
                return "IB00501";
            }

            var lastId = int.Parse(lastCustomer.CusId.Substring(4));
            var newId = lastId + 1;

            return "IB00" + newId.ToString("D3");
        }

        public async Task<bool> IsUserExistsAsync(int userId)
        {
            return await _context.Users.AnyAsync(u => u.UserId == userId);
        }

        public async Task<bool> IsCustomerDetailsUniqueAsync(string aadhaarNo, string panCardNo)
        {
            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(c =>
                c.AadhaarNo == aadhaarNo || c.PanCardNo == panCardNo
            );

            return existingCustomer == null;
        }

        public async Task<Customer> GetCustomerByIdAsync(string cusId)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.CusId == cusId);
        }
    }
}
