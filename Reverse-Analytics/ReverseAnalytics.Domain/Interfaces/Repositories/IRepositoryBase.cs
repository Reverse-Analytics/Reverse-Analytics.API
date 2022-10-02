namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> FindAllAsync(int pageSize = 0, int pageNumber = 0);
        Task<T?> FindByIdAsync(int id);
        T Create(T entity);
        void CreateRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void Delete(int id);
        void DeleteRange(IEnumerable<int> ids);
        Task<bool> SaveChangesAsync();
        Task<bool> EntityExistsAsync(int id);
    }
}
