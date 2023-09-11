using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Sale;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings
{
    public class SaleMapping : Profile
    {
        public SaleMapping()
        {
            CreateMap<Sale, SaleDto>()
                .ForMember(x => x.SaleDetails, r => r.MapFrom(e => e.SaleDetails));
            CreateMap<SaleDto, Sale>();
            CreateMap<SaleForCreateDto, Sale>();
            CreateMap<SaleForUpdateDto, Sale>();
        }
    }
}
