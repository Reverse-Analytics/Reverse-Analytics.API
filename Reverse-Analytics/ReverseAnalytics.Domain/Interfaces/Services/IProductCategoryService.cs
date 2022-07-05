using ReverseAnalytics.Domain.DTOs.ProductCategory;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryDto>?> GetAllProductCategoriesAsync(string? searchString);
        Task<ProductCategoryDto?> GetProductCategoryByIdAsync(int id);
        Task<ProductCategoryDto?> CreateProductCategoryAsync(ProductCategoryForCreateDto productCategoryToCreate);
        Task UpdateProductCategoryAsync(ProductCategoryForUpdateDto productCategoryToUpdate);
        Task DeleteProductCategoryAsync(int productCategoryId);
    }
}
