using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IProductCategoryRepository _productCategory;
        public IProductCategoryRepository ProductCategory => _productCategory ?? 
            new ProductCategoryRepository(_context);

        private readonly IProductRepository _product;
        public IProductRepository Product => _product ?? 
            new ProductRepository(_context);

        private readonly ICustomerRepository _customerRepository;
        public ICustomerRepository Customer => _customerRepository ?? 
            new CustomerRepository(_context);

        private readonly ICustomerPhoneRepository _customerPhone;
        public ICustomerPhoneRepository CustomerPhone => _customerPhone ??
            new CustomerPhoneRepository(_context);

        public CommonRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException("Parameter context cannot be null.");
            _productCategory = new ProductCategoryRepository(_context);
            _product = new ProductRepository(_context);
            _customerRepository = new CustomerRepository(_context);
            _customerPhone = new CustomerPhoneRepository(_context);
        }
    }
}
