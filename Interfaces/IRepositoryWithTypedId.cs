using Microsoft.EntityFrameworkCore.Storage;
using TestKSK.Data.Interfaces;

namespace TestKSK.Interfaces
{
    public interface IRepositoryWithTypedId<T, TId> where T : IEntityWithTypedId<TId>
    {
        IQueryable<T> Query();

        void Add(T entity);

        void AddRange(IEnumerable<T> entity);

        IDbContextTransaction BeginTransaction();

        void SaveChanges();

        Task SaveChangesAsync();

        void Remove(T entity);
        void ClearChangeTracker();
    }
}
