using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.QueryParameters;
using ReverseAnalytics.Infrastructure.Extensions;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Infrastructure.Repositories;

public class ProductRepository(ApplicationDbContext context) : RepositoryBase<Product>(context), IProductRepository
{
    public async Task<PaginatedList<Product>> FindAllAsync(ProductQueryParameters queryParameters)
    {
        ArgumentNullException.ThrowIfNull(queryParameters);

        var query = _context.Products.AsQueryable();

        if (queryParameters.Price.HasValue)
        {
            query = query.Where(x => x.SalePrice == queryParameters.Price || x.SupplyPrice == queryParameters.Price);
        }

        if (queryParameters.CategoryId.HasValue)
        {
            query = query.Where(x => x.CategoryId == queryParameters.CategoryId);
        }

        if (!string.IsNullOrWhiteSpace(queryParameters.SearchQuery))
        {
            query = query.Where(x => x.Name.Contains(queryParameters.SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                x.Code.Contains(queryParameters.SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                (x.Description != null && x.Description.Contains(queryParameters.SearchQuery, StringComparison.OrdinalIgnoreCase)));
        }

        return await query.ToPaginatedListAsync(queryParameters.PageNumber, queryParameters.PageSize);
    }
}
