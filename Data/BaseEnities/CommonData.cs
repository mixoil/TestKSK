using TestKSK.Data.Interfaces;

namespace TestKSK.Data.BaseEnities
{
    public abstract class CommonDataWithId : IEntityWithTypedId<Guid>
    {
        public Guid Id { get; set; }
    }
}
