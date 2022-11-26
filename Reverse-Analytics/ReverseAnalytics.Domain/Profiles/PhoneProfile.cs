using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Phone;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Profiles
{
    public class PhoneProfile : Profile
    {
        public PhoneProfile()
        {
            CreateMap<Phone, PhoneDto>();
            CreateMap<PhoneDto, Phone>();
            CreateMap<PhoneForCreateDto, Phone>();
            CreateMap<PhoneForUpdateDto, Phone>();
        }
    }
}
