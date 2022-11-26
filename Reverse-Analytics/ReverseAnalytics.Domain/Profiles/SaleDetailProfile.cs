using AutoMapper;
using ReverseAnalytics.Domain.DTOs.SaleDetail;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Profiles
{
    internal class SaleDetailProfile : Profile
    {
        public SaleDetailProfile()
        {
            CreateMap<SaleDetail, SaleDetailDto>();
            CreateMap<SaleDetailDto, SaleDetail>();
            CreateMap<SaleDetailForCreateDto, SaleDetail>();
            CreateMap<SaleDetailForUpdateDto, SaleDetail>();
        }
    }
}
