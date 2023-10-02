using AutoMapper;
using ReverseAnalytics.Domain.DTOs.SaleItem;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings
{
    public class SaleItemMapping : Profile
    {
        public SaleItemMapping()
        {
            CreateMap<SaleItem, SaleItemDto>();
            CreateMap<SaleItemDto, SaleItem>();
            CreateMap<SaleItemForCreateDto, SaleItem>();
            CreateMap<SaleItemForUpdateDto, SaleItem>();
        }
    }
}
