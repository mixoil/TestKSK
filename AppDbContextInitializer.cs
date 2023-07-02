using Microsoft.EntityFrameworkCore;
using TestKSK.Data;
using TestKSK.Interfaces;

namespace TestKSK
{
    public class AppDbContextInitializer : IDbContextInitializer
    {
        private readonly AppDbContext appDbContext;
        private readonly IRepository<VendingMachine> vendingMachineRepository;

        public AppDbContextInitializer(AppDbContext appDbContext,
            IRepository<VendingMachine> vendingMachineRepository)
        {
            this.appDbContext = appDbContext;
            this.vendingMachineRepository = vendingMachineRepository;
        }

        public async Task Migrate(CancellationToken cancellationToken = default)
        {
            await appDbContext.Database.MigrateAsync(cancellationToken);
        }

        public async Task Seed(CancellationToken cancellationToken = default)
        {
            if (await vendingMachineRepository.Query().AnyAsync())
                return;
            var newVendingMachine = new VendingMachine
            {
                MoneyUnits = new List<MoneyUnit>
                {
                    new MoneyUnit { Denomination = 1, IsAvailable = true },
                    new MoneyUnit { Denomination = 2, IsAvailable = true },
                    new MoneyUnit { Denomination = 5, IsAvailable = true },
                    new MoneyUnit { Denomination = 10, IsAvailable = true }
                }
            };
            vendingMachineRepository.Add(newVendingMachine);
            await vendingMachineRepository.SaveChangesAsync();
        }
    }
}
