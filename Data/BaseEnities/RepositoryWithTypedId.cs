using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TestKSK.Data.Interfaces;
using TestKSK.Interfaces;

namespace TestKSK.Data.BaseEnities
{
    public class RepositoryWithTypedId<T, TId> : IRepositoryWithTypedId<T, TId> where T : class, IEntityWithTypedId<TId>
    {
        public RepositoryWithTypedId(IDbContextFactory<AppDbContext> dbFactory)
        {
            Context = dbFactory.CreateDbContext();
            DbSet = Context.Set<T>();
        }

        protected DbContext Context { get; }

        protected DbSet<T> DbSet { get; }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entity)
        {
            DbSet.AddRange(entity);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public IQueryable<T> Query()
        {
            return DbSet;
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public void ClearChangeTracker()
        {
            Context.ChangeTracker.Clear();
        }

        ~RepositoryWithTypedId()
        {
            Context?.Dispose();
        }
    }
}
