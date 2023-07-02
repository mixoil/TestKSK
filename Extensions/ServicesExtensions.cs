using Microsoft.EntityFrameworkCore;
using TestKSK.Data.BaseEnities;
using TestKSK.Interfaces;
using TestKSK.Mappings;
using TestKSK.Services;

namespace TestKSK.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IVendingMachineService, VendingMachineService>();
            services.AddScoped<IDbContextInitializer, AppDbContextInitializer>(); 
            services.AddAutoMapper(typeof(VendingMappings));
            return services;
        }
    }
}
