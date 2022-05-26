namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> FindAll();
        Task<T> FindById(int id);
        T Create(T entity);
        Task CreateRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveChangesAsync();
    }
}
