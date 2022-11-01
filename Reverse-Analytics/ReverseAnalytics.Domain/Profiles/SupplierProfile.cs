using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Supplier;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Profiles
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<Supplier, SupplierDto>();
            CreateMap<SupplierDto, Supplier>();
            CreateMap<SupplierForCreateDto, Supplier>();
            CreateMap<SupplierForUpdateDto, Supplier>();
        }
    }
}
