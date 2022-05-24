using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Infrastructure.Persistence;
using ReverseAnalytics.Infrastructure.Persistence.Interceptors;

namespace Reverse_Analytics.Api.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

#if DEBUG
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
#else
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), builder =>
                builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
#endif

            return services;
        }
    }
}
