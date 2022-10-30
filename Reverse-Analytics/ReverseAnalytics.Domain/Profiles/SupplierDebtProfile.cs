using AutoMapper;
using ReverseAnalytics.Domain.DTOs.SupplierDebt;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Profiles
{
    internal class SupplierDebtProfile : Profile
    {
        public SupplierDebtProfile()
        {
            CreateMap<SupplierDebt, SupplierDebtDto>();
            CreateMap<SupplierDebtDto, SupplierDebt>();
            CreateMap<SupplierDebtForCreateDto, SupplierDebt>();
            CreateMap<SupplierDebtForUpdateDto, SupplierDebt>();
        }
    }
}
