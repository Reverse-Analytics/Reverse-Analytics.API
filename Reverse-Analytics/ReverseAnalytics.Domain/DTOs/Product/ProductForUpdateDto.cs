namespace ReverseAnalytics.Domain.DTOs.Product
{
    public class ProductForUpdateDto
    {
        public int Id { get; private set; }
        public string ProductName { get; set; }
        public double Volume { get; set; }
        public double Weight { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }

        public int CategoryId { get; set; }
    }
}
