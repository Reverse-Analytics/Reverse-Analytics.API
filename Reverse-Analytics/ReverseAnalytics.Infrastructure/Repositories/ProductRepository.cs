using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Infrastructure.Repositories;

public class ProductRepository(ApplicationDbContext context) : RepositoryBase<Product>(context), IProductRepository
{
}
