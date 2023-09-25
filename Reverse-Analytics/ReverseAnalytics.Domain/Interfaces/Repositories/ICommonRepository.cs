namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ICommonRepository
    {
        public ICustomerRepository Customer { get; }
        public IProductCategoryRepository ProductCategory { get; }
        public IProductRepository Product { get; }
        public IRefundItemRepository RefundDetail { get; }
        public IRefundRepository Refund { get; }
        public ISaleDebtRepository SaleDebt { get; }
        public ISaleItemRepository SaleDetail { get; }
        public ISaleRepository Sale { get; }
        public ISupplierRepository Supplier { get; }
        public ISupplyDebtRepository SupplyDebt { get; }
        public ISupplyItemRepository SupplyDetail { get; }
        public ISupplyRepository Supply { get; }

        public Task<int> SaveChangesAsync();
    }
}
