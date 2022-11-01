using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Supply;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Profiles
{
    public class SupplyProfile : Profile
    {
        public SupplyProfile()
        {
            CreateMap<Supply, SupplyDto>();
            CreateMap<SupplyDto, Supply>();
            CreateMap<SupplyForCreate, Supply>();
            CreateMap<SupplyForUpdate, Supply>();
        }
    }
}
