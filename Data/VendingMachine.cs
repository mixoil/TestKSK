using TestKSK.Data.BaseEnities;
using TestKSK.Models;

namespace TestKSK.Data
{
    public class VendingMachine : CommonDataWithId
    {
        public uint UserBalance { get; set; }
        public IEnumerable<Beverage> Beverages { get; set; }
        public IEnumerable<MoneyUnit> MoneyUnits { get; set; }
    }
}
