namespace ReverseAnalytics.Domain.DTOs.ProductCategory
{
    public class ProductCategoryForUpdateDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public ProductCategoryForUpdateDto(int id, string categoryName)
        {
            Id = id;
            CategoryName = categoryName;
        }
    }
}
