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
        if (string.IsNullOrWhiteSpace(queryParameters.SearchQuery))
        {
            return await base.FindAllAsync(queryParameters);
        }

        var entities = await _context.ProductCategories
            .Where(x => x.Name.Contains(queryParameters.SearchQuery))
            .ToPaginatedListAsync(queryParameters.PageNumber, queryParameters.PageSize);

        return entities;
    }
}
