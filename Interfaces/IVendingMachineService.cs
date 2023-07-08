using TestKSK.Models;
using TestKSK.Models.Requests;

namespace TestKSK.Interfaces
{
    public interface IVendingMachineService
    {
        Task<VendingMachineModel> GetVendingMachineModel();
        Task<UserActionResult> UpdateUserBalance(UpdateBalanceRequest request); 
        Task<AdminPanelModel> GetAdminModel();
        Task<UserActionResult> UpdateState(AdminPanelModel adminModel);
    }
}
