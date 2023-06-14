namespace TestKSK.Models
{
    public class VendingMachineModel
    {
        public uint UserBalance { get; set; }
        public BeverageModel[] Beverages { get; set; }
        public MoneyUnitModel[] MoneyUnits { get; set; }
    }
}
