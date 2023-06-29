using AutoMapper;
using ReverseAnalytics.Domain.DTOs.SupplyDebt;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings
{
    public class SupplyDebtMapping : Profile
    {
        public SupplyDebtMapping()
        {
            CreateMap<SupplyDebt, SupplyDebtDto>();
            CreateMap<SupplyDebtDto, SupplyDebt>();
            CreateMap<SupplyDebtForCreateDto, SupplyDebt>();
            CreateMap<SupplyDebtForUpdateDto, SupplyDebt>();
        }
    }
}
