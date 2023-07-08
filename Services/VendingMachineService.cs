using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestKSK.Data;
using TestKSK.Interfaces;
using TestKSK.Models;
using TestKSK.Models.Requests;

namespace TestKSK.Services
{
    public class VendingMachineService : IVendingMachineService
    {
        private readonly IRepository<VendingMachine> vendingMachineRepository;
        private readonly IMapper mapper;

        public VendingMachineService(IRepository<VendingMachine> vendingMachineRepository,
            IMapper mapper)
        {
            this.vendingMachineRepository = vendingMachineRepository;
            this.mapper = mapper;
        }

        public async Task<VendingMachineModel> GetVendingMachineModel()
        {
            var vendingMachine = await GetVendingMachine();
            return mapper.Map<VendingMachineModel>(vendingMachine);
        }

        public async Task<UserActionResult> UpdateUserBalance(UpdateBalanceRequest request)
        {
            var result = new UserActionResult();
            if (request == null)
            {
                result.ErrorMsg = "Request missing!";
                return result;
            }
            var machine = await GetVendingMachine();
            var moneyUnit = machine.MoneyUnits.FirstOrDefault(u => u.Denomination == request.Denomination);
            if (moneyUnit == null || !moneyUnit.IsAvailable)
            {
                result.ErrorMsg = "Money unit is missing or unavailable!";
                return result;
            }
            machine.UserBalance += moneyUnit.Denomination;
            await vendingMachineRepository.SaveChangesAsync();
            result.Succeeded = true;
            return result;
        }

        public async Task<VendingMachine> GetVendingMachine()
        {
            return await vendingMachineRepository.Query()
                .Include(m => m.MoneyUnits)
                .Include(m => m.Beverages)
                .FirstOrDefaultAsync();
        }

        public async Task<AdminPanelModel> GetAdminModel()
        {
            var vendingMachine = await GetVendingMachineModel();
            return mapper.Map<AdminPanelModel>(vendingMachine);
        }

        public async Task<UserActionResult> UpdateState(AdminPanelModel adminModel)
        {
            var result = new UserActionResult();
            var vendingMachine = mapper.Map<VendingMachine>(adminModel);
            var dbVendingMachine = await GetVendingMachine();
            result.Succeeded = true;
            return result;
        }

        private void UpdateVendingMachineDbModel(VendingMachine dbModel, AdminPanelModel adminModel)
        {
            
        }
    }
}
