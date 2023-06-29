using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Refund;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings
{
    public class RefundMapping : Profile
    {
        public RefundMapping()
        {
            CreateMap<Refund, RefundDto>();
            CreateMap<RefundDto, Refund>();
            CreateMap<RefundForCreateDto, Refund>();
            CreateMap<RefundForUpdateDto, Refund>();
        }
    }
}
