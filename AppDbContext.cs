using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TestKSK.Data.BaseEnities;
using TestKSK.Data.Interfaces;

namespace TestKSK
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Type> typeToRegisters = Assembly.GetAssembly(typeof(AppDbContext))
                .DefinedTypes.Select(t => t.AsType()).ToList();
            RegisterEntities(modelBuilder, typeToRegisters);
            RegisterConvention(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private static void RegisterEntities(ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters)
        {
            var entityTypes = typeToRegisters.Where(x => x.GetTypeInfo().GetInterfaces()
                .Any(i => i == typeof(IEntity)) && !x.GetTypeInfo().IsAbstract);
            foreach (var type in entityTypes)
            {
                modelBuilder.Entity(type);
            }
        }

        private static void RegisterConvention(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.ClrType.Namespace != null)
                    modelBuilder.Entity(entity.Name).ToTable(entity.ClrType.Name);
            }

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.SetNull;
            }
        }
    }
}
