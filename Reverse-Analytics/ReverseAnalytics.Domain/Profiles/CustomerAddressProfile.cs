using AutoMapper;
using ReverseAnalytics.Domain.DTOs.CustomerAddress;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Profiles
{
    public class CustomerAddressProfile : Profile
    {
        public CustomerAddressProfile()
        {
            CreateMap<CustomerAddress, CustomerAddressDto>();
            CreateMap<CustomerAddressDto, CustomerAddress>();
            CreateMap<CustomerAddressForCreateDto, CustomerAddress>();
            CreateMap<CustomerAddressForUpdateDto, CustomerAddress>();
        }
    }
}
