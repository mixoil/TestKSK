using TestKSK.Data.Interfaces;

namespace TestKSK.Data
{
    public class MoneyUnit : IEntity
    {
        public uint Denomination { get; set; }
        public bool IsAvailable { get; set; }
    }
}
