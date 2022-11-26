using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Order;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<OrderForCreate, Order>();
            CreateMap<OrderForUpdate, Order>();
        }
    }
}
