using AutoMapper;
using ReverseAnalytics.Domain.DTOs.RefundDetail;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings
{
    public class RefundDetailMapping : Profile
    {
        public RefundDetailMapping()
        {
            CreateMap<RefundDetail, RefundDetailDto>();
            CreateMap<RefundDetailDto, RefundDetail>();
            CreateMap<RefundDetailForCreateDto, RefundDetail>();
            CreateMap<RefundDetailForUpdateDto, RefundDetail>();
        }
    }
}
