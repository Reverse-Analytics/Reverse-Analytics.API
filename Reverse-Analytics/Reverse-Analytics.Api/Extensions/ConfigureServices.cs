using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Infrastructure.Persistence;
using ReverseAnalytics.Infrastructure.Persistence.Interceptors;

namespace Reverse_Analytics.Api.Extensions;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditInterceptor>();
        services.AddScoped<TransactionsInterceptor>();
        var provider = configuration.GetValue("Provider", "SqlServer");

        services.AddDbContext<ApplicationDbContext>(
            options => _ = provider switch
            {
                "Sqlite" => options.UseSqlite(
                    configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("ReverseAnalytics.Migrations.Sqlite")),

                "SqlServer" => options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("ReverseAnalytics.Migrations.SqlServer")),

                _ => throw new InvalidOperationException($"Unsupported provider: {provider}.")
            });

#if DEBUG

#else
        

        services.AddDbContext<ApplicationIdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultIdentityConnection"), builder =>
            builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
#endif
        //services.AddScoped<ICommonRepository, CommonRepository>();
        //services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        //services.AddScoped<IProductRepository, ProductRepository>();
        //services.AddScoped<ICustomerRepository, CustomerRepository>();
        //services.AddScoped<ISaleRepository, SaleRepository>();
        //services.AddScoped<ISaleItemRepository, SaleItemRepository>();

        return services;
    }

    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
