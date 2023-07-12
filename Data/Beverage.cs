using TestKSK.Data.BaseEnities;

namespace TestKSK.Data
{
    public class Beverage : CommonDataWithId
    {
        public string Name { get; set; }
        public uint Price { get; set; }
        public uint Count { get; set; }
        public Guid VendingMachineId { get; set; }
        public VendingMachine VendingMachine { get; set; }
    }
}
