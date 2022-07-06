using ReverseAnalytics.Domain.DTOs.ProductCategory;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryDto>?> GetProductCategoriesAsync();
        Task<IEnumerable<ProductCategoryDto>?> GetProductCategoriesAsync(string? searchString);
        Task<ProductCategoryDto?> GetProductCategoryByIdAsync(int id);
        Task<ProductCategoryDto?> CreateProductCategoryAsync(ProductCategoryForCreateDto productCategoryToCreate);
        Task UpdateProductCategoryAsync(ProductCategoryForUpdateDto productCategoryToUpdate);
        Task DeleteProductCategoryAsync(int productCategoryId);
    }
}
