using Microsoft.EntityFrameworkCore;
using TestKSK.Data.Interfaces;
using TestKSK.Interfaces;

namespace TestKSK.Data.BaseEnities
{
    public class Repository<T> : RepositoryWithTypedId<T, Guid>, IRepository<T>
    where T : class, IEntityWithTypedId<Guid>
    {
        public Repository(IDbContextFactory<AppDbContext> dbFactory) : base(dbFactory)
        {
        }
    }
}
