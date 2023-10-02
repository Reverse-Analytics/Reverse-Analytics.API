namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ICommonRepository
    {
        public ICustomerRepository Customer { get; }
        public IProductCategoryRepository ProductCategory { get; }
        public IProductRepository Product { get; }
        public IRefundItemRepository RefundItem { get; }
        public IRefundRepository Refund { get; }
        public ISaleItemRepository SaleItem { get; }
        public ISaleRepository Sale { get; }
        public ISupplierRepository Supplier { get; }
        public ISupplyDebtRepository SupplyDebt { get; }
        public ISupplyItemRepository SupplyItem { get; }
        public ISupplyRepository Supply { get; }

        public Task<int> SaveChangesAsync();
    }
}
