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
            services.AddScoped<ICustomerPhoneService, CustomerPhoneService>();
            services.AddScoped<ICustomerDebtService, CustomerDebtService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<ISupplierPhoneService, SupplierPhoneService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
