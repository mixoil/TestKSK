using TestKSK.Data.Interfaces;

namespace TestKSK.Repository.BaseRepositories
{
    public interface IRepositoryWithTypedId<T, TId> where T : IEntityWithTypedId<TId>
    {
        IQueryable<T> Query();

        void Add(T entity);

        void AddRange(IEnumerable<T> entity);

        void SaveChanges();

        Task SaveChangesAsync();

        void Remove(T entity);
        void ClearChangeTracker();
    }

}
