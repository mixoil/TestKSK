using Microsoft.EntityFrameworkCore;
using TestKSK.Data.BaseEnities;
using TestKSK.Interfaces;

namespace TestKSK.Extensions
{
    public static class DataExtensions
    {
        public static IServiceCollection AddCustomizedDataStore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            UpdateDatabase(services);
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }

        private static void UpdateDatabase(IServiceCollection services)
        {
            using (var serviceScope = services.BuildServiceProvider()
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<AppDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
