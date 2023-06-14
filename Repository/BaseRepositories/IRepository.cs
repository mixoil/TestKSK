using TestKSK.Data.Interfaces;

namespace TestKSK.Repository.BaseRepositories
{
    public interface IRepository<T> : IRepositoryWithTypedId<T, Guid> where T : IEntityWithTypedId<Guid>
    {
    }

}
