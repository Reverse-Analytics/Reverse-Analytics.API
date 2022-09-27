using ReverseAnalytics.Domain.DTOs.Product;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>?> GetProductsAsync();
        Task<IEnumerable<ProductDto>?> GetProductsAsync(string? searchString, int? categoryId, int pageSize, int pageNumber);
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(ProductForCreateDto productToCreate);
        Task UpdateProductAsync(ProductForUpdateDto productToUpdate);
        Task DeleteProductAsync(int id);
    }
}
