using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.ResourceParameters;
using ReverseAnalytics.Infrastructure.Helpers;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Infrastructure.Repositories
{
    public class ProductRepository(ApplicationDbContext context) : RepositoryBase<Product>(context), IProductRepository
    {
        public async Task<IEnumerable<Product>> FindAllAsync(ProductResourceParamters resourceParameters)
        {
            if (resourceParameters is null)
            {
                return await _context.Products.ToListAsync();
            }

            var query = BuildQuery(resourceParameters);

            var pagedList = await PagedList<Product>.CreateAsync(
                query,
                resourceParameters.PageNumber,
                resourceParameters.PageSize);

            return pagedList;
        }

        private IQueryable<Product> BuildQuery(ProductResourceParamters resourceParameters)
        {
            var query = _context.Products
                .AsQueryable()
                .AsNoTracking();

            if (!string.IsNullOrEmpty(resourceParameters.SearchQuery))
            {
                query = query.Where(
                    x => x.Name.Contains(resourceParameters.SearchQuery) ||
                    x.Code.Contains(resourceParameters.SearchQuery));
            }

            if (resourceParameters.PriceGreaterThan is not null)
            {
                query = query.Where(x => x.SalePrice > resourceParameters.PriceGreaterThan);
            }

            if (resourceParameters.PriceLessThan is not null)
            {
                query = query.Where(x => x.SalePrice < resourceParameters.PriceLessThan);
            }

            if (resourceParameters.PriceEqualTo is not null)
            {
                query = query.Where(x => x.SalePrice == resourceParameters.PriceEqualTo);
            }

            if (resourceParameters.CategoryId is not null)
            {
                query = query.Where(x => x.CategoryId == resourceParameters.CategoryId);
            }

            return query.OrderBy(x => x.Name);
        }
    }
}
