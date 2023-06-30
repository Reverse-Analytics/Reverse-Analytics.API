using ReverseAnalytics.Domain.Interfaces.Services;
using ReverseAnalytics.Services;

namespace Reverse_Analytics.Api.Extensions
{
    public static class RegisterDependencyInjections
    {
        public static void RegisterDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRefundDetailService, RefundDetailService>();
            services.AddScoped<IRefundService, RefundService>();
            services.AddScoped<ISaleDebtService, SaleDebtService>();
            services.AddScoped<ISaleDetailService, SaleDetailService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<ISupplyDebtService, SupplyDebtService>();
            services.AddScoped<ISupplyDetailService, SupplyDetailService>();
            services.AddScoped<ISupplyService, SupplyService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
