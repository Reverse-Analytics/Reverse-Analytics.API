using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reverse_Analytics.Api.Filters;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;
using ReverseAnalytics.Infrastructure.Persistence;
using ReverseAnalytics.Infrastructure.Persistence.Interceptors;
using ReverseAnalytics.Repositories;
using ReverseAnalytics.Services;

namespace Reverse_Analytics.Api.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

#if DEBUG
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
#else
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), builder =>
                builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddDbContext<ApplicationIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultIdentityConnection"), builder =>
                builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
#endif
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<ISaleItemRepository, SaleItemRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISaleItemservice, SaleItemservice>();
            services.AddScoped<ISaleService, SaleService>();

            return services;
        }

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        public static IServiceCollection ConfigureValidationFilter(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);
            services.AddScoped<ValidationFilterAttribute>();

            return services;
        }
    }
}
