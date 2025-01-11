using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Services
{
    public interface IFundTransferService
    {
        Task<Beneficiary> AddBeneficiaryAsync(Beneficiary beneficiary);

        Task<Beneficiary> GetBeneficiaryByIdAsync(int id);

        Task<Beneficiary> DeleteBeneficiaryAsync(int id);

        Task<ShowAccountBalance> ShowAccountBalanceAsync(int userId);

        Task TransferFundsAsync(
            FundTransferBeneficiary fundTransferBeneficiary,
            decimal amount,
            string remarks
        );
        Task TransferFundsWithinBankAsync(
            int senderUserId,
            WithinBankBeneficiary withinBankBeneficiary,
            decimal amount,
            string remarks,
            string transMode
        );
    }
}
