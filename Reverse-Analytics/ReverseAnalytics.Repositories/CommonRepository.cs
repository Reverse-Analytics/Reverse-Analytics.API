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

        private readonly ICustomerRepository _customer;
        public ICustomerRepository Customer => _customer ?? 
            new CustomerRepository(_context);

        private readonly ICustomerPhoneRepository _customerPhone;
        public ICustomerPhoneRepository CustomerPhone => _customerPhone ??
            new CustomerPhoneRepository(_context);

        private readonly ICustomerDebtRepository _customerDebt;
        public ICustomerDebtRepository CustomerDebt => _customerDebt ??
            new CustomerDebtRepository(_context);

        private readonly IOrderRepository _order;
        public IOrderRepository Order => _order ??
            new OrderRepository(_context);

        private readonly IOrderItemRepository _orderItem;
        public IOrderItemRepository OrderItem => _orderItem ??
            new OrderItemRepository(_context);

        private readonly ISupplierRepository _supplier;
        public ISupplierRepository Supplier => _supplier ??
            new SupplierRepository(_context);

        public CommonRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException("Parameter context cannot be null.");
            _productCategory = new ProductCategoryRepository(_context);
            _product = new ProductRepository(_context);
            _customer = new CustomerRepository(_context);
            _customerPhone = new CustomerPhoneRepository(_context);
            _customerDebt = new CustomerDebtRepository(_context);
            _order = new OrderRepository(_context);
            _orderItem = new OrderItemRepository(_context);
            _supplier = new SupplierRepository(context);
        }

        public async Task<int> SaveChangesAsync()
        {
            int savedChanges = await _context.SaveChangesAsync();

            return savedChanges;
        }
    }
}
