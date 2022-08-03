using ReverseAnalytics.Domain.DTOs.City;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ICityService
    {
        Task<IEnumerable<CityDto>?> GetAllCities(string? searchString);
        Task<CityDto?> GetCityById(int id);
        Task<CityDto?> CreateCity(CityForCreateDto cityToCreate);
        Task UpdateCityAsync(CityForUpdateDto cityToUpdate);
        Task DeleteCityAsync(int id);
    }
}
