namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ICommonRepository
    {
        public ICustomerRepository Customer { get; }
        public IProductCategoryRepository ProductCategory { get; }
        public IProductRepository Product { get; }
        public IRefundDetailRepository RefundDetail { get; }
        public IRefundRepository Refund { get; }
        public ISaleDebtRepository SaleDebt { get; }
        public ISaleDetailRepository SaleDetail { get; }
        public ISaleRepository Sale { get; }
        public ISupplierRepository Supplier { get; }
        public ISupplyDebtRepository SupplyDebt { get; }
        public ISupplyDetailRepository SupplyDetail { get; }
        public ISupplyRepository Supply { get; }

        public Task<int> SaveChangesAsync();
    }
}
