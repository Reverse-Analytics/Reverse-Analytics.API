using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Address;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>();
            CreateMap<AddressForCreateDto, Address>();
            CreateMap<AddressForUpdateDto, Address>();
        }
    }
}
