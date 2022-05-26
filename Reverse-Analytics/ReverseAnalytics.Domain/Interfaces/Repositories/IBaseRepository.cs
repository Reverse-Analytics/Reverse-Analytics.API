namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> FindAll();
        Task<T?> FindById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveChangesAsync();
        Task<bool> EntityExists(int id);
    }
}
