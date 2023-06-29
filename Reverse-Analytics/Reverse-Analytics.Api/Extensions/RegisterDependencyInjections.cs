using ReverseAnalytics.Domain.Interfaces.Services;
using ReverseAnalytics.Services;

namespace Reverse_Analytics.Api.Extensions
{
    public static class RegisterDependencyInjections
    {
        public static void RegisterDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<ISaleDetailService, SaleDetailService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<ISupplyService, SupplyService>();
            services.AddScoped<ISupplyDetailService, SupplyDetailService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
