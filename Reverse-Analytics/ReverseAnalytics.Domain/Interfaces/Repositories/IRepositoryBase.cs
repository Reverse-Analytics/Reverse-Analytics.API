namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        IEnumerable<T> FindAllAsync();
        Task<T?> FindByIdAsync(int id);
        T Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        Task<bool> SaveChangesAsync();
        Task<bool> EntityExistsAsync(int id);
    }
}
