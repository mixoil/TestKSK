namespace TestKSK.Models
{
    public class MoneyUnitModel
    {
        public Guid Id { get; set; }
        public uint Denomination { get; set; }
        public bool IsAvailable { get; set; }
    }
}
