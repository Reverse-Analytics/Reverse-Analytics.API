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

        private readonly ISupplierPhoneRepository _supplierPhone;
        public ISupplierPhoneRepository SupplierPhone => _supplierPhone ??
            new SupplierPhoneRepository(_context);

        private readonly ISupplierDebtRepository _supplierDebt;
        public ISupplierDebtRepository SupplierDebt => _supplierDebt ??
            new SupplierDebtRepository(_context);

        private readonly ISupplyRepository _supply;
        public ISupplyRepository Supply => _supply ??
            new SupplyRepository(_context);

        private readonly ISupplyDetailRepository _supplyDetail;
        public ISupplyDetailRepository SupplyDetail => _supplyDetail ??
            new SupplyDetailRepository(_context);

        private readonly IUserRepository _user;
        public IUserRepository User => _user ??
            new UserRepository(_context);

        public CommonRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _productCategory = new ProductCategoryRepository(context);
            _product = new ProductRepository(context);
            _customer = new CustomerRepository(context);
            _customerPhone = new CustomerPhoneRepository(context);
            _customerDebt = new CustomerDebtRepository(context);
            _order = new OrderRepository(context);
            _orderItem = new OrderItemRepository(context);
            _supplier = new SupplierRepository(context);
            _supplierPhone = new SupplierPhoneRepository(context);
            _supplierDebt = new SupplierDebtRepository(context);
            _supply = new SupplyRepository(context);
            _supplyDetail = new SupplyDetailRepository(context);
            _user = new UserRepository(context);
        }

        public async Task<int> SaveChangesAsync()
        {
            int savedChanges = await _context.SaveChangesAsync();

            return savedChanges;
        }
    }
}
