using AutoMapper;
using ReverseAnalytics.Domain.DTOs.SupplyDetail;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Profiles
{
    public class SupplyDetailProfile : Profile
    {
        public SupplyDetailProfile()
        {
            CreateMap<SupplyDetail, SupplyDetailDto>();
            CreateMap<SupplyDetailDto, SupplyDetail>();
            CreateMap<SupplyDetailForCreateDto, SupplyDetail>();
            CreateMap<SupplyDetailForUpdateDto, SupplyDetail>();
        }
    }
}
