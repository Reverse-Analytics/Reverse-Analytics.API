using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.DTOs.ProductCategory;
using ReverseAnalytics.Domain.ResourceParameters;

namespace ReverseAnalytics.Domain.Interfaces.Services;

public interface IProductCategoryService
{
    Task<IEnumerable<ProductCategoryDto>> GetAllAsync();
    Task<IEnumerable<ProductCategoryDto>> GetAllAsync(PaginatedQueryParameters queryParameters);
    Task<IEnumerable<ProductDto>> GetProductsAsync(int categoryId);
    Task<ProductCategoryDto> GetByIdAsync(int id);
    Task<ProductCategoryDto> CreateAsync(ProductCategoryForCreateDto categoryToCreate);
    Task<ProductCategoryDto> UpdateAsync(ProductCategoryForUpdateDto categoryToUpdate);
    Task DeleteAsync(int id);
}
