using AutoMapper;
using ReverseAnalytics.Domain.DTOs.CustomerPhone;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Profiles
{
    public class CustomerPhoneProfile : Profile
    {
        public CustomerPhoneProfile()
        {
            CreateMap<CustomerPhone, CustomerPhoneDto>();
            CreateMap<CustomerPhoneDto, CustomerPhone>();
            CreateMap<CustomerPhoneForCreate, CustomerPhone>();
            CreateMap<CustomerPhoneForUpdate, CustomerPhone>();
        }
    }
}
