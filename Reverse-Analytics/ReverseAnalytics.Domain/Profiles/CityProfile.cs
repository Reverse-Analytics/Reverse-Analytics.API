using AutoMapper;
using ReverseAnalytics.Domain.DTOs.City;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Profiles
{
    internal class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDto>();
            CreateMap<CityDto, City>();
            CreateMap<CityForCreateDto, City>();
            CreateMap<CityForUpdateDto, City>();
        }
    }
}
