using AutoMapper;
using ReverseAnalytics.Doman.DTOs.RefundItem;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings
{
    public class RefundItemMapping : Profile
    {
        public RefundItemMapping()
        {
            CreateMap<RefundItem, RefundItemDto>();
            CreateMap<RefundItemDto, RefundItem>();
            CreateMap<RefundItemForCreateDto, RefundItem>();
            CreateMap<RefundItemForUpdateDto, RefundItem>();
        }
    }
}
