namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> FindAllAsync();
        Task<T?> FindByIdAsync(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveChangesAsync();
        Task<bool> EntityExistsAsync(int id);
    }
}
