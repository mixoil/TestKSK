namespace TestKSK.Models
{
    public class AdminPanelModel
    {
        public uint UserBalance { get; set; }
        public BeverageModel[] Beverages { get; set; }
        public MoneyUnitModel[] MoneyUnits { get; set; }
    }
}
