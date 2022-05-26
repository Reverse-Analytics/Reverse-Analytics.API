using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<List<ProductCategory>> FindAllProductCategoriesAsync(string? searchString)
        {
            var productCategories = _context.ProductCategories
                .Include(pc => pc.Products)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                productCategories = productCategories.Where(pc => pc.CategoryName == searchString);
            }

            return await productCategories.ToListAsync();
        }

        public async Task<ProductCategory?> FindProductCategoryByIdAsync(int id)
        {
            return await FindByIdAsync(id);
        }

        public ProductCategory CreateProductCategory(ProductCategory categoryToCreate)
        {
            Create(categoryToCreate);

            return categoryToCreate;
        }

        public void UpdateProductCategory(ProductCategory categoryToUpdate)
        {
            Update(categoryToUpdate);
        }

        public void DeleteProductCategory(ProductCategory categoryToDelete)
        {
            Delete(categoryToDelete);
        }
    }
}
