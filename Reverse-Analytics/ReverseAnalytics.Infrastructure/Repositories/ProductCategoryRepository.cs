using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.ResourceParameters;
using ReverseAnalytics.Infrastructure.Extensions;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Infrastructure.Repositories;

public class ProductCategoryRepository(ApplicationDbContext context) : RepositoryBase<ProductCategory>(context), IProductCategoryRepository
{
    public override async Task<PaginatedList<ProductCategory>> FindAllAsync(PaginatedQueryParameters queryParameters)
    {
        var query = _context.ProductCategories.AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParameters.SearchQuery))
        {
            query = query.Where(x => x.Name.Contains(queryParameters.SearchQuery, StringComparison.OrdinalIgnoreCase));
        }

        var entities = await query
            .Include(x => x.Products)
            .AsNoTracking()
            .ToPaginatedListAsync(queryParameters.PageNumber, queryParameters.PageSize);

        return entities;
    }
}
