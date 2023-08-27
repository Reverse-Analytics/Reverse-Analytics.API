using AutoMapper;
using ReverseAnalytics.Domain.DTOs.SaleDebt;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings
{
    public class SaleDebtMapping : Profile
    {
        public SaleDebtMapping()
        {
            CreateMap<SaleDebt, SaleDebtDto>()
                .ForMember(x => x.Receipt, e => e.MapFrom(r => r.Sale.Receipt))
                .ForMember(x => x.Customer, e => e.MapFrom(r => r.Sale.Customer.FullName))
                .ForMember(x => x.DebtDate, e => e.MapFrom(r => r.Sale.SaleDate))
                .ForMember(x => x.SoldBy, e => e.MapFrom(r => r.Sale.SoldBy))
                .ForMember(x => x.TotalDue, e => e.MapFrom(r => Math.Round(r.TotalDue, 2)))
                .ForMember(x => x.TotalPaid, e => e.MapFrom(r => Math.Round(r.TotalPaid, 2)))
                .ForMember(x => x.ClosedBy, e => e.MapFrom(r => r.LastModifiedBy));
            CreateMap<SaleDebtDto, SaleDebt>();
            CreateMap<SaleDebtForCreateDto, SaleDebt>();
            CreateMap<SaleDebtForUpdateDto, SaleDebt>();
        }
    }
}
