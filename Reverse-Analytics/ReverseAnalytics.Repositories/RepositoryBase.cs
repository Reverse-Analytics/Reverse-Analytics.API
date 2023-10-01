using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;

        protected RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> FindAllAsync(int pageSize = 0, int pageNumber = 0)
        {
            if (pageSize > 0 && pageNumber > 0)
            {
                return await _context.Set<T>()
                                 .AsNoTracking()
                                 .Skip(pageSize * (pageNumber - 1))
                                 .Take(pageSize)
                                 .OrderBy(x => x.Id)
                                 .ToListAsync();
            }

            return await _context.Set<T>()
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<T?> FindByIdAsync(int id)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public T Create(T entity)
        {
            return _context.Set<T>().Add(entity).Entity;
        }

        public void CreateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public void Update(T entity)
        {
            var entityExists = _context.Set<T>().Contains(entity);

            if (!entityExists)
            {
                throw new NotFoundException($"There is no Entity {typeof(T)} with id: {entity.Id}.");
            }

            _context.Set<T>().Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);

            if (entity == null)
            {
                throw new NotFoundException($"There is no Entity {typeof(T)} with id: {id}.");
            }

            Delete(entity);
        }

        public async void DeleteRange(IEnumerable<int> ids)
        {
            foreach (var id in ids)
            {
                bool entityExists = await EntityExistsAsync(id);

                if (!entityExists)
                {
                    throw new Exception($"There is no Entity Type {typeof(T)} with id: {id}.");
                }

                Delete(id);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<bool> EntityExistsAsync(int id)
        {
            return await _context.Set<T>()
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(s => s.Id == id) != null;
        }
    }
}
