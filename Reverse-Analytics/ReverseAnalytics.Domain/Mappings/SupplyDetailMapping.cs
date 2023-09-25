using AutoMapper;
using ReverseAnalytics.Domain.DTOs.SupplyItem;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings
{
    public class SupplyDetailMapping : Profile
    {
        public SupplyDetailMapping()
        {
            CreateMap<SupplyItem, SupplyItemDto>();
            CreateMap<SupplyItemDto, SupplyItem>();
            CreateMap<SupplyItemForCreateDto, SupplyItem>();
            CreateMap<SupplyItemForUpdateDto, SupplyItem>();
        }
    }
}
