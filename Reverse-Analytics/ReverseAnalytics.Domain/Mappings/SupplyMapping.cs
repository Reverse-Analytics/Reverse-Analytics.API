using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Supply;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings
{
    internal class SupplyMapping : Profile
    {
        public SupplyMapping()
        {
            CreateMap<Supply, SupplyDto>();
            CreateMap<SupplyDto, Supply>();
            CreateMap<SupplyForCreateDto, Supply>();
            CreateMap<SupplyForUpdateDto, Supply>();
        }
    }
}
