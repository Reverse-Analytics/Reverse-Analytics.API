using ReverseAnalytics.Domain.DTOs.City;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ICityService
    {
        Task<IEnumerable<CityDto>?> GetAllCitiesAsync(string? searchString);
        Task<CityDto?> GetCityByIdAsync(int id);
        Task<CityDto?> CreateCityAsync(CityForCreateDto cityToCreate);
        Task UpdateCityAsync(CityForUpdateDto cityToUpdate);
        Task DeleteCityAsync(int id);
    }
}
