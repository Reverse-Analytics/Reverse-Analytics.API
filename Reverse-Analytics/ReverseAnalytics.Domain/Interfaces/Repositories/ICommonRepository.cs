namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ICommonRepository
    {
        public IDebtRepository Debt { get; }
        public IProductCategoryRepository ProductCategory { get; }
        public IProductRepository Product { get; }
        public ICustomerRepository Customer { get; }
        public ISaleRepository Sale { get; }
        public ISaleDetailRepository SaleDetail { get; }
        public ISupplierRepository Supplier { get; }
        public ISupplyRepository Supply { get; }
        public ISupplyDetailRepository SupplyDetail { get; }

        public Task<int> SaveChangesAsync();
    }
}
