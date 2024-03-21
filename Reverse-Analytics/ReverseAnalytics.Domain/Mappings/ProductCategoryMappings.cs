using AutoMapper;
using ReverseAnalytics.Domain.DTOs.ProductCategory;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings;

public class ProductCategoryMappings : Profile
{
    public ProductCategoryMappings()
    {
        CreateMap<ProductCategory, ProductCategoryDto>()
            .ForCtorParam(nameof(ProductCategoryDto.NumberOfProducts), r => r.MapFrom(e => e.Products.Count));
        CreateMap<ProductCategoryForCreateDto, ProductCategory>();
        CreateMap<ProductCategoryForUpdateDto, ProductCategory>();
    }
}
