namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ICommonRepository
    {
        public IProductCategoryRepository ProductCategory { get; }
        public IProductRepository Product { get; }
        public ICustomerRepository Customer { get; }
        public ICustomerPhoneRepository CustomerPhone { get; }
        public ICustomerDebtRepository CustomerDebt { get; }
        public IOrderRepository Order { get; }
        public IOrderItemRepository OrderItem { get; }
        public ISupplierRepository Supplier { get; }
        public ISupplierPhoneRepository SupplierPhone { get; }
        public ISupplierDebtRepository SupplierDebt { get; }
        public ISupplyRepository Supply { get; }

        public Task<int> SaveChangesAsync();
    }
}
