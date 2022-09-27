using AutoMapper;
using ReverseAnalytics.Domain.DTOs.OrderDetail;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Profiles
{
    internal class OrderDetailProfile : Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<OrderDetail, OrderDetailDto>();
            CreateMap<OrderDetailDto, OrderDetail>();
            CreateMap<OrderDetailForCreate, OrderDetail>();
            CreateMap<OrderDetailForUpdate, OrderDetail>();
        }
    }
}
