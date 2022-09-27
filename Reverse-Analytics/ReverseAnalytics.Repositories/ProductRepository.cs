using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Product>> FindAllProductsAsync(string? searchString, int? categoryId, int pageSize, int pageNumber)
        {
            var products = _context.Products.AsNoTracking().AsQueryable();

            // Search
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.ProductName.Contains(searchString));
            }

            // Filter
            if (categoryId != null)
            {
                products = products.Where(p => p.CategoryId == categoryId);
            }

            // Sort
            products = products.OrderBy(p => p.ProductName);

            // Pagination
            products = products.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize);

            return await products.ToListAsync();
        }
    }
}
