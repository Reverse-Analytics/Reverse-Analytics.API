using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.ResourceParameters;

namespace ReverseAnalytics.Domain.Interfaces.Repositories;

public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> FindAllAsync<TParam>(TParam resourceParameters) where TParam : BaseResourceParameters;

    Task<TEntity> FindByIdAsync(int id);

    Task<TEntity> Create(TEntity entity);

    Task<IEnumerable<TEntity>> Create(IEnumerable<TEntity> entities);

    Task Update(TEntity entity);

    Task Update(IEnumerable<TEntity> entities);

    Task Delete(int id);

    Task Delete(IEnumerable<int> ids);

    Task<int> SaveChangesAsync();

    Task<bool> EntityExistsAsync(TEntity entity);
}
