using AutoMapper;
using ReverseAnalytics.Domain.DTOs.CustomerDebt;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Profiles
{
    public class CustomerDebtProfile : Profile
    {
        public CustomerDebtProfile()
        {
            CreateMap<CustomerDebt, CustomerDebtDto>();
            CreateMap<CustomerDebtDto, CustomerDebt>();
            CreateMap<CustomerDebtForCreate, CustomerDebt>();
            CreateMap<CustomerDebtForUpdate, CustomerDebt>();
        }
    }
}
