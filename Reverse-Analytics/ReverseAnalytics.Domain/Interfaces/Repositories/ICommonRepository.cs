namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ICommonRepository
    {
        public IProductCategoryRepository ProductCategory { get; }
        public IProductRepository Product { get; }
    }
}
