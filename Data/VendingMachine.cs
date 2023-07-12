using TestKSK.Data.BaseEnities;
using TestKSK.Models;

namespace TestKSK.Data
{
    public class VendingMachine : CommonDataWithId
    {
        public uint UserBalance { get; set; }
        public IList<Beverage> Beverages { get; set; }
        public IList<MoneyUnit> MoneyUnits { get; set; }
    }
}
