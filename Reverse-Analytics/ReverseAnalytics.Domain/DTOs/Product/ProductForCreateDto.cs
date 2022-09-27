namespace ReverseAnalytics.Domain.DTOs.Product
{
    public class ProductForCreateDto
    {
        public string ProductName { get; set; } = string.Empty;
        public double Volume { get; set; }
        public double Weight { get; set; }
        public decimal SupplyPrice { get; set; }
        public decimal SalePrice { get; set; }

        public int CategoryId { get; set; }
    }
}
