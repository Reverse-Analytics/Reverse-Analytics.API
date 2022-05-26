using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private ApplicationDbContext _context;

        private IProductCategoryRepository _productCategoryRepository;
        public IProductCategoryRepository ProductCategoryRepository => _productCategoryRepository ?? new ProductCategoryRepository(_context);

        public CommonRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException("Parameter context cannot be null.");
        }
    }
}
