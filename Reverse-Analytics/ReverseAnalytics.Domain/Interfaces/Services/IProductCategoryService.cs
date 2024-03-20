using ReverseAnalytics.Domain.DTOs.ProductCategory;
using ReverseAnalytics.Domain.ResourceParameters;

namespace ReverseAnalytics.Domain.Interfaces.Services;

public interface IProductCategoryService
{
    Task<IEnumerable<ProductCategoryDto>> GetProductsAsync();
    Task<IEnumerable<ProductCategoryDto>> GetProductsAsync(PaginatedQueryParameters queryParameters);
}
