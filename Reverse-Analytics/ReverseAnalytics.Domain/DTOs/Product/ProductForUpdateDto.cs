using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Product
{
    public class ProductForUpdateDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public UnitOfMeasurement UnitOfMeasurement { get; set; }
        public double? Volume { get; set; }
        public double? Weight { get; set; }
        public decimal SupplyPrice { get; set; }
        public decimal SalePrice { get; set; }

        public int CategoryId { get; set; }
    }
}
