using AutoMapper;
using ReverseAnalytics.Domain.DTOs.SaleDetail;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings
{
    public class SaleDetailMapping : Profile
    {
        public SaleDetailMapping()
        {
            CreateMap<SaleDetail, SaleDetailDto>();
            CreateMap<SaleDetailDto, SaleDetail>();
            CreateMap<SaleDetailForCreateDto, SaleDetail>();
            CreateMap<SaleDetailForUpdateDto, SaleDetail>();
        }
    }
}
