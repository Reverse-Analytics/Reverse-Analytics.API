using AutoMapper;
using ReverseAnalytics.Domain.DTOs.SupplyDetail;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings
{
    public class SupplyDetailMapping : Profile
    {
        public SupplyDetailMapping()
        {
            CreateMap<SupplyDetail, SupplyDetailDto>();
            CreateMap<SupplyDetailDto, SupplyDetail>();
            CreateMap<SupplyDetailForCreateDto, SupplyDetail>();
            CreateMap<SupplyDetailForUpdateDto, SupplyDetail>();
        }
    }
}
