using AutoMapper;
using ReverseAnalytics.Domain.DTOs.ProductCategory;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappers
{
    public class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile()
        {
            CreateMap<ProductCategory, ProductCategoryDto>();
            CreateMap<ProductCategoryDto, ProductCategory>();
            CreateMap<ProductCategoryForCreateDto, ProductCategory>();
            CreateMap<ProductCategoryForUpdateDto, ProductCategory>();
        }
    }
}
