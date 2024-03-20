using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Infrastructure.Repositories;

public class ProductCategoryRepository(ApplicationDbContext context) : RepositoryBase<ProductCategory>(context), IProductCategoryRepository
{
}
