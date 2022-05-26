namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ICommonRepository
    {
        public IProductCategoryRepository ProductCategoryRepository { get; }
    }
}
