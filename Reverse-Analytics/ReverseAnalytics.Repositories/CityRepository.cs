using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class CityRepository : RepositoryBase<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<City>> FindAllCitiesAsync(string? searchString)
        {
            var cities = _context.Cities.AsQueryable();
            var query = cities.ToQueryString();

            if (!string.IsNullOrEmpty(searchString))
            {
                cities = cities.Where(c => c.CityName.Contains(searchString));
            }

            return await cities.ToListAsync();
        }
    }
}
