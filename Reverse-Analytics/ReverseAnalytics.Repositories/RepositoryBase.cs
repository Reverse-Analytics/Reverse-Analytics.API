using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        protected RepositoryBase(ApplicationDbContext context)
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

        public T Create(T entity)
        {
            return _context.Set<T>().Add(entity).Entity;
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);

            if(entity != null)
            {
                Delete(entity);
            }

            throw new NotFoundException($"There is no entity type {typeof(T)} with id: {id}");
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
