namespace ReverseAnalytics.Domain.DTOs.ProductCategory
{
    public class ProductCategoryForCreateDto
    {
        public string CategoryName { get; set; }

        public ProductCategoryForCreateDto(string categoryName)
        {
            CategoryName = categoryName;
        }
    }
}
