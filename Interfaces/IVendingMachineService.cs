using TestKSK.Models;
using TestKSK.Models.Requests;

namespace TestKSK.Interfaces
{
    public interface IVendingMachineService
    {
        Task<VendingMachineModel> GetVendingMachineModel();
        Task UpdateUserBalance(UpdateBalanceRequest request);
    }
}
