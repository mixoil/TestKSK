using TestKSK.Models;
using TestKSK.Models.Requests;

namespace TestKSK.Interfaces
{
    public interface IVendingMachineService
    {
        Task<VendingMachineModel> GetVendingMachineModel();
        Task<UserActionResult> UpdateUserBalance(UpdateBalanceRequest request);
        Task<UserActionResult> SwitchMoneyUnitAvailability(UpdateMoneyUnitAvailabilityRequest request);
        Task<AdminPanelModel> GetAdminModel();
        Task<UserActionResult> UpdateState(AdminPanelModel adminModel);
        Task<BeverageModel> GetBeverageModel(Guid id);
        Task<UserActionResult> AddBeverage(BeverageModel beverageModel);
        Task<UserActionResult> EditBeverage(BeverageModel beverageModel);
    }
}
