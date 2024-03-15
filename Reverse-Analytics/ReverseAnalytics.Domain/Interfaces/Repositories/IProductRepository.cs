using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.ResourceParameters;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<IEnumerable<Product>> FindAllAsync(ProductResourceParamters resourceParameters);
    }
}
