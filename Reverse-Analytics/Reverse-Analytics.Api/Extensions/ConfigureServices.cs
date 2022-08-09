using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;
using ReverseAnalytics.Infrastructure.Persistence.Interceptors;
using ReverseAnalytics.Repositories;

namespace Reverse_Analytics.Api.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();


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
