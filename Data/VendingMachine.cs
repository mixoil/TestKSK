using TestKSK.Data.BaseEnities;
using TestKSK.Models;

namespace TestKSK.Data
{
    public class VendingMachine : CommonData
    {
        public uint UserBalance { get; set; }
        public BeverageModel[] Beverages { get; set; }
        public MoneyUnitModel[] MoneyUnits { get; set; }
    }
}
