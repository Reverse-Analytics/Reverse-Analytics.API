using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Inventory;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Profiles
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile()
        {
            CreateMap<Inventory, InventoryDto>();
            CreateMap<InventoryDto, Inventory>();
            CreateMap<InventoryForCreateDto, Inventory>();
            CreateMap<InventoryForUpdateDto, Inventory>();
        }
    }
}
