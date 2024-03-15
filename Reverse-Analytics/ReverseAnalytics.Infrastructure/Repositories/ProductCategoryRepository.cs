using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.ResourceParameters;
using ReverseAnalytics.Infrastructure.Helpers;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Infrastructure.Repositories
{
    public class ProductCategoryRepository(ApplicationDbContext context) : RepositoryBase<ProductCategory>(context), IProductCategoryRepository
    {
        public async Task<IEnumerable<ProductCategory>> FindAllAsync(CategoryResourceParameters resourceParameters)
        {
            if (resourceParameters is null)
            {
                return await _context.ProductCategories.ToListAsync();
            }

            var query = _context.ProductCategories
                .AsQueryable()
                .AsNoTracking();

            if (!string.IsNullOrEmpty(resourceParameters.SearchQuery))
            {
                query = query.Where(x => x.Name.Contains(resourceParameters.SearchQuery));
            }

            query = query.OrderBy(x => x.Name);

            var pagedList = await PagedList<ProductCategory>.CreateAsync(
                query,
                resourceParameters.PageNumber,
                resourceParameters.PageSize);

            return pagedList;
        }
    }
}
