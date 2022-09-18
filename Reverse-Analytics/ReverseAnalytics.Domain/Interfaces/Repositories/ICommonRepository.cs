namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ICommonRepository
    {
        public IProductCategoryRepository ProductCategory { get; }
        public IProductRepository Product { get; }
        public ICustomerRepository Customer { get; }
        public ICustomerPhoneRepository CustomerPhone { get; }
    }
}
