using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IProductCategoryRepository : IBaseRepository<ProductCategory>
    {
        Task<List<ProductCategory>> FindAllProductCategoriesAsync(string? searchString);
        Task<ProductCategory?> FindProductCategoryByIdAsync(int id);
        ProductCategory CreateProductCategory(ProductCategory categoryToCreate);
        void UpdateProductCategory(ProductCategory categoryToUpdate);
        void DeleteProductCategory(ProductCategory categoryToDelete);
    }
}
