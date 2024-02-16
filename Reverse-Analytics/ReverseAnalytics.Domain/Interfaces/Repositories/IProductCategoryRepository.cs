using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.ResourceParameters;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IProductCategoryRepository : IRepositoryBase<ProductCategory>
    {
        Task<IEnumerable<ProductCategory>> FindAllAsync(CategoryResourceParameters resourceParameters);
    }
}
