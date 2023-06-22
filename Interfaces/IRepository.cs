using TestKSK.Data.Interfaces;

namespace TestKSK.Interfaces
{
    public interface IRepository<T> : IRepositoryWithTypedId<T, Guid> where T : IEntityWithTypedId<Guid>
    {
    }
}
