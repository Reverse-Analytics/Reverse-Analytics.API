using AutoMapper;
using ReverseAnalytics.Domain.DTOs.ProductCategory;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings
{
    public class ProductCategoryMapping : Profile
    {
        public ProductCategoryMapping()
        {
            CreateMap<ProductCategory, ProductCategoryDto>();
            CreateMap<ProductCategoryDto, ProductCategory>();
            CreateMap<ProductCategoryForCreateDto, ProductCategory>();
            CreateMap<ProductCategoryForUpdateDto, ProductCategory>();
        }
    }
}
