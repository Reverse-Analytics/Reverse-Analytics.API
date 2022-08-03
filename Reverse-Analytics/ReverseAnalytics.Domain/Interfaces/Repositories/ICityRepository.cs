using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ICityRepository : IRepositoryBase<City>
    {
        public Task<IEnumerable<City>> FindAllCitiesAsync(string? searchString);
    }
}
