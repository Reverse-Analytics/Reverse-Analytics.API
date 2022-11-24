using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IAddressRepository _address;
        public IAddressRepository Address => _address ??
            new AddressRepository(_context);

        private readonly IDebtRepository _debt;
        public IDebtRepository Debt => _debt ??
            new DebtRepository(_context);

        private readonly IProductCategoryRepository _productCategory;
        public IProductCategoryRepository ProductCategory => _productCategory ?? 
            new ProductCategoryRepository(_context);

        private readonly IProductRepository _product;
        public IProductRepository Product => _product ?? 
            new ProductRepository(_context);

        private readonly ICustomerRepository _customer;
        public ICustomerRepository Customer => _customer ?? 
            new CustomerRepository(_context);

        private readonly IOrderRepository _order;
        public IOrderRepository Order => _order ??
            new OrderRepository(_context);

        private readonly IOrderItemRepository _orderItem;
        public IOrderItemRepository OrderItem => _orderItem ??
            new OrderItemRepository(_context);

        private readonly ISupplierRepository _supplier;
        public ISupplierRepository Supplier => _supplier ??
            new SupplierRepository(_context);

        private readonly ISupplyRepository _supply;
        public ISupplyRepository Supply => _supply ??
            new SupplyRepository(_context);

        private readonly ISupplyDetailRepository _supplyDetail;
        public ISupplyDetailRepository SupplyDetail => _supplyDetail ??
            new SupplyDetailRepository(_context);

        public CommonRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _address = new AddressRepository(context);
            _debt = new DebtRepository(context);
            _productCategory = new ProductCategoryRepository(context);
            _product = new ProductRepository(context);
            _customer = new CustomerRepository(context);
            _order = new OrderRepository(context);
            _orderItem = new OrderItemRepository(context);
            _supplier = new SupplierRepository(context);
            _supply = new SupplyRepository(context);
            _supplyDetail = new SupplyDetailRepository(context);
        }

        public async Task<int> SaveChangesAsync()
        {
            int savedChanges = await _context.SaveChangesAsync();

            return savedChanges;
        }
    }
}
