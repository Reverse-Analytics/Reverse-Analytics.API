using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Debt;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Profiles
{
    public class DebtProfile : Profile
    {
        public DebtProfile()
        {
            CreateMap<Debt, DebtDto>();
            CreateMap<DebtDto, Debt>();
            CreateMap<DebtForCreateDto, Debt>();
            CreateMap<DebtForUpdateDto, Debt>();
        }
    }
}
