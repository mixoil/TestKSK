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

        public async Task UpdateUserBalance(UpdateBalanceRequest request)
        {
            if (request == null)
                return;
            var machine = await GetVendingMachine();
            var moneyUnit = machine.MoneyUnits.FirstOrDefault(u => u.Denomination == request.Denomination);
            if (moneyUnit == null || !moneyUnit.IsAvailable)
                return;
            machine.UserBalance += moneyUnit.Denomination;
            await vendingMachineRepository.SaveChangesAsync();
        }

        private async Task<VendingMachine> GetVendingMachine()
        {
            return await vendingMachineRepository.Query()
                .Include(m => m.MoneyUnits)
                .Include(m => m.Beverages)
                .FirstOrDefaultAsync();
        }
    }
}
