using Microsoft.EntityFrameworkCore;
using TestKSK.Data.BaseEnities;
using TestKSK.Interfaces;
using static System.Formats.Asn1.AsnWriter;

namespace TestKSK.Extensions
{
    public static class DataExtensions
    {
        public static async Task AddCustomizedDataStore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            await UpdateDatabase(services);
        }

        private static async Task UpdateDatabase(IServiceCollection services)
        {
            using (var serviceScope = services.BuildServiceProvider()
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;
                var dbInitializers = serviceProvider.GetServices<IDbContextInitializer>();
                foreach (var dbInitializer in dbInitializers)
                {
                    await dbInitializer.Migrate();
                    await dbInitializer.Seed();
                }
            }
        }
    }
}
