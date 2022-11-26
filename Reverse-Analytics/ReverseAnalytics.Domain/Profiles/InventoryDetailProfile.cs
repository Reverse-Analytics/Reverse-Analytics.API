using AutoMapper;
using ReverseAnalytics.Domain.DTOs.InventoryDetail;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Profiles
{
    public class InventoryDetailProfile : Profile
    {
        public InventoryDetailProfile()
        {
            CreateMap<InventoryDetail, InventoryDetailDto>();
            CreateMap<InventoryDetailDto, InventoryDetail>();
            CreateMap<InventoryDetailForCreateDto, InventoryDetail>();
            CreateMap<InventoryForUpdateDto, InventoryDetail>();
        }
    }
}
