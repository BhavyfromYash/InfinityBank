using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Repository
{
    public class FundTransferRepository : IFundTransferService
    {
        private readonly BankDbContext _context;

        public FundTransferRepository(BankDbContext context)
        {
            _context = context;
        }

        public async Task<Beneficiary> AddBeneficiaryAsync(Beneficiary beneficiary)
        {
            var user = await _context.Beneficiaries.AddAsync(beneficiary);
            await _context.SaveChangesAsync();
            return user.Entity;
        }

        public async Task<Beneficiary> GetBeneficiaryAsync(int id)
        {
            return await _context.Beneficiaries.FirstOrDefaultAsync(b => b.BenId == id);
        }

        public async Task<Beneficiary> DeleteBeneficiaryAsync(int id)
        {
            var beneficiary = await _context.Beneficiaries.FindAsync(id);
            if (beneficiary != null)
            {
                _context.Beneficiaries.Remove(beneficiary);
            }
            await _context.SaveChangesAsync();

            return beneficiary;
        }
    }
}
