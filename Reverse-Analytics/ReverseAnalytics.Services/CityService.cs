using AutoMapper;
using ReverseAnalytics.Domain.DTOs.City;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class CityService : ICityService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public CityService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CityDto>?> GetAllCities(string? searchString)
        {
            try
            {
                var cities = await _repository.City.FindAllCitiesAsync(searchString);

                if(cities is null)
                {
                    return null;
                }

                var cityDtos = _mapper.Map<IEnumerable<CityDto>?>(cities);

                if(cityDtos is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(City)} Entities to {typeof(CityDto)}.");
                }

                return cityDtos;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error retrieving cities.", ex);
            }
        }
        
        public async Task<CityDto?> GetCityById(int id)
        {
            try
            {
                var city = await _repository.City.FindByIdAsync(id);

                if(city is null)
                {
                    return null;
                }

                var cityDto = _mapper.Map<CityDto>(city);

                if(cityDto is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(City)} Entity to {typeof(CityDto)}.");
                }

                return cityDto;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error retrieving City with id: {id}", ex);
            }
        }

        public async Task<CityDto?> CreateCity(CityForCreateDto cityToCreate)
        {
            try
            {
                if(cityToCreate is null)
                {
                    throw new ArgumentNullException(nameof(cityToCreate));
                }

                var cityEntity = _mapper.Map<City>(cityToCreate);

                if(cityEntity is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(CityForCreateDto)} to {typeof(City)}.");
                }

                var createdCity = _repository.City.Create(cityEntity);
                await _repository.City.SaveChangesAsync();

                var cityDto = _mapper.Map<CityDto>(createdCity);

                if(cityDto is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(City)} to {typeof(CityDto)}.");
                }

                return cityDto;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error while adding new City.", ex);
            }
        }

        public async Task UpdateCityAsync(CityForUpdateDto cityToUpdate)
        {
            try
            {
                if (cityToUpdate is null)
                {
                    throw new ArgumentNullException(nameof(cityToUpdate));
                }

                var cityEntity = _mapper.Map<City>(cityToUpdate);

                if (cityEntity is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(CityForUpdateDto)} to {typeof(City)}");
                }

                _repository.City.Update(cityEntity);
                await _repository.City.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error updating city.", ex);
            }
        }

        public async Task DeleteCityAsync(int id)
        {
            try
            {
                
                _repository.City.Delete(id);
                await _repository.City.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error deleting city with id: {id}.", ex);
            }
        }
    }
}
