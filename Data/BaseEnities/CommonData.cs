using TestKSK.Data.Interfaces;

namespace TestKSK.Data.BaseEnities
{
    public class CommonData : IEntityWithTypedId<Guid>
    {
        public Guid Id { get; set; }
    }
}
