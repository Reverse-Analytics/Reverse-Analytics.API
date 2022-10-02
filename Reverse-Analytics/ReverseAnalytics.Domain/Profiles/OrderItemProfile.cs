using AutoMapper;
using ReverseAnalytics.Domain.DTOs.OrderItem;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Profiles
{
    internal class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderDetail, OrderItemDto>();
            CreateMap<OrderItemDto, OrderDetail>();
            CreateMap<OrderItemForCreate, OrderDetail>();
            CreateMap<OrderItemForUpdate, OrderDetail>();
        }
    }
}
