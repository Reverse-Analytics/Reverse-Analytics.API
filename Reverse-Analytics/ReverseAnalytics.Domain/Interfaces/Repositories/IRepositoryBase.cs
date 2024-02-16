using ReverseAnalytics.Domain.ResourceParameters;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> FindAllAsync(ResourceParametersBase resourceParameters);
        Task<IEnumerable<T>> FindAllAsync(Func<T, bool> predicate);
        Task<T> FindByIdAsync(int id);
        Task<T> Create(T entity);
        Task<IEnumerable<T>> CreateRange(IEnumerable<T> entities);
        Task Update(T entity);
        Task UpdateRange(IEnumerable<T> entities);
        Task Delete(int id);
        Task DeleteRange(IEnumerable<int> ids);
        Task<int> SaveChangesAsync();
        Task<bool> EntityExistsAsync(T entity);
    }
}
