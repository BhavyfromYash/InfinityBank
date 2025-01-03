using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Services
{
    public interface IFundTransferService
    {
        Task<Beneficiary> AddBeneficiaryAsync(Beneficiary beneficiary);

        Task<Beneficiary> GetBeneficiaryAsync(int id);

        Task<Beneficiary> DeleteBeneficiaryAsync(int id);
    }
}
