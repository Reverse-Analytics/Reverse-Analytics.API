using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        protected BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> FindAllAsync()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T?> FindByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<bool> EntityExistsAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id) != null;
        }
    }
}
