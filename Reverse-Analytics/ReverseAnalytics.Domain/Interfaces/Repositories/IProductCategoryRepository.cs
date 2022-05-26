using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IProductCategoryRepository : IBaseRepository<ProductCategory>
    {
        Task<List<ProductCategory>> FindAllProductCategories(string? searchString);
        Task<ProductCategory?> FindProductCategoryById(int id);
        Task<ProductCategory> CreateProductCategory(ProductCategory categoryToCreate);
        void UpdateProductCategory(ProductCategory categoryToUpdate);
        void DeleteProductCategory(int id);
    }
}
