using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<List<ProductCategory>> FindAllProductCategoriesAsync(string? searchString)
        {
            var productCategories = _context.ProductCategories
                .Include(x => x.Products)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                productCategories = productCategories.Where(pc => pc.CategoryName == searchString);
            }

            return await productCategories.ToListAsync();
        }
    }
}
