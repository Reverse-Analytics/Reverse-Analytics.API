using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Sale;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Profiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<Sale, SaleDto>();
            CreateMap<SaleDto, Sale>();
            CreateMap<SaleForCreateDto, Sale>();
            CreateMap<SaleForUpdateDto, Sale>();
        }
    }
}
