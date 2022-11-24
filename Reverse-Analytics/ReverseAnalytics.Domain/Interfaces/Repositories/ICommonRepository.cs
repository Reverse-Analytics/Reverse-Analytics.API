namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ICommonRepository
    {
        public IAddressRepository Address { get; }
        public IDebtRepository Debt { get; }
        public IProductCategoryRepository ProductCategory { get; }
        public IProductRepository Product { get; }
        public ICustomerRepository Customer { get; }
        public IOrderRepository Order { get; }
        public IOrderItemRepository OrderItem { get; }
        public ISupplierRepository Supplier { get; }
        public ISupplyRepository Supply { get; }
        public ISupplyDetailRepository SupplyDetail { get; }

        public Task<int> SaveChangesAsync();
    }
}
