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
                .ForMember(x => x.SaleItems, r => r.MapFrom(e => e.SaleItems));
            CreateMap<SaleDto, Sale>();
            CreateMap<SaleForCreateDto, Sale>();
            CreateMap<SaleForUpdateDto, Sale>();
        }
    }
}
