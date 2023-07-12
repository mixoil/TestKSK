using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestKSK.Data;
using TestKSK.Interfaces;
using TestKSK.Models;
using TestKSK.Models.Requests;
using TestKSK.Models.Results;

namespace TestKSK.Services
{
    public class VendingMachineService : IVendingMachineService
    {
        private readonly IRepository<VendingMachine> vendingMachineRepository;
        private readonly IRepository<Beverage> beverageRepository;
        private readonly IRepository<MoneyUnit> moneyUnitRepository;
        private readonly IMapper mapper;

        public VendingMachineService(IRepository<VendingMachine> vendingMachineRepository,
            IRepository<Beverage> beverageRepository, IMapper mapper,
            IRepository<MoneyUnit> moneyUnitRepository)
        {
            this.vendingMachineRepository = vendingMachineRepository;
            this.beverageRepository = beverageRepository;
            this.mapper = mapper;
            this.moneyUnitRepository = moneyUnitRepository;
        }

        public async Task<VendingMachineModel> GetVendingMachineModel()
        {
            var vendingMachine = await GetVendingMachine();
            return mapper.Map<VendingMachineModel>(vendingMachine);
        }

        public async Task<BeverageBuyResult> BuyBeverage(BeverageBuyRequest request)
        {
            var result = new BeverageBuyResult();
            if (request == null)
            {
                result.ErrorMsg = "Request missing!";
                return result;
            }
            var machine = await GetVendingMachine();
            var dbBeverage = machine.Beverages.FirstOrDefault(b => b.Id == request.Id);
            if (dbBeverage == null)
            {
                result.ErrorMsg = "Beverage not found!";
                return result;
            }
            if (dbBeverage.Count < 1)
            {
                result.ErrorMsg = "Beverage solded out!";
                return result;
            }
            if (dbBeverage.Price > machine.UserBalance)
            {
                result.ErrorMsg = "User balance too low!";
                return result;
            }

            result.Change = machine.UserBalance - dbBeverage.Price;
            dbBeverage.Count--;
            machine.UserBalance = 0;

            await vendingMachineRepository.SaveChangesAsync();

            result.Succeeded = true;
            return result;
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

        public async Task<UserActionResult> SwitchMoneyUnitAvailability(
            UpdateMoneyUnitAvailabilityRequest request)
        {
            var result = new UserActionResult();
            if (request == null)
            {
                result.ErrorMsg = "Request missing!";
                return result;
            }
            var moneyUnit = await GetMoneyUnit(request.Denomination);
            if (moneyUnit == null)
            {
                result.ErrorMsg = "Money unit is missing!";
                return result;
            }
            moneyUnit.IsAvailable = !moneyUnit.IsAvailable;
            await moneyUnitRepository.SaveChangesAsync();
            result.Succeeded = true;
            return result;
        }

        public async Task<VendingMachine> GetVendingMachine()
        {
            return await vendingMachineRepository.Query()
                .Include(m => m.MoneyUnits.OrderBy(u => u.Denomination))
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

        #region Admin

        public async Task<BeverageModel> GetBeverageModel(Guid id)
        {
            var beverage = await beverageRepository.Query().FirstOrDefaultAsync(b => b.Id == id);
            if (beverage == null)
                return null;
            return mapper.Map<BeverageModel>(beverage);
        }

        private async Task<Beverage> GetBeverage(Guid id)
        {
            return await beverageRepository.Query()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        private async Task<MoneyUnit> GetMoneyUnit(uint denomination)
        {
            return await moneyUnitRepository.Query()
                .FirstOrDefaultAsync(b => b.Denomination == denomination);
        }

        public async Task<UserActionResult> AddBeverage(BeverageModel beverageModel)
        {
            var result = new UserActionResult();
            if (string.IsNullOrWhiteSpace(beverageModel.Name))
            {
                result.ErrorMsg = "Beverage name is invalid";
                return result;
            }
            var dbVendingMachine = await GetVendingMachine();
            var dbBeverage = mapper.Map<Beverage>(beverageModel);

            dbVendingMachine.Beverages.Add(dbBeverage);
            await vendingMachineRepository.SaveChangesAsync();

            result.Succeeded = true;
            return result;
        }

        public async Task<UserActionResult> EditBeverage(BeverageModel beverageModel)
        {
            var result = new UserActionResult();
            if (string.IsNullOrWhiteSpace(beverageModel.Name))
            {
                result.ErrorMsg = "Beverage name is invalid";
                return result;
            }
            var dbBeverage = await GetBeverage(beverageModel.Id);

            dbBeverage.Name = beverageModel.Name;
            dbBeverage.Price = beverageModel.Price;
            dbBeverage.Count = beverageModel.Count;
            
            await beverageRepository.SaveChangesAsync();

            result.Succeeded = true;
            return result;
        }

        #endregion
    }
}
