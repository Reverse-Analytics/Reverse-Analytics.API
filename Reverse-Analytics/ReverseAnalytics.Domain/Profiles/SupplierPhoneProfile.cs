using AutoMapper;
using ReverseAnalytics.Domain.DTOs.SupplierPhone;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Profiles
{
    public class SupplierPhoneProfile : Profile
    {
        public SupplierPhoneProfile()
        {
            CreateMap<SupplierPhone, SupplierPhoneDto>();
            CreateMap<SupplierPhoneDto, SupplierPhone>();
            CreateMap<SupplierPhoneForCreate, SupplierPhone>();
            CreateMap<SupplierPhoneForUpdate, SupplierPhone>();
        }
    }
}
