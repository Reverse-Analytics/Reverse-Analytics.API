using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.DTOs.Sale;

namespace ReverseAnalytics.Domain.DTOs.SaleItem
{
    public class SaleItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public double DiscountPercentage { get; set; }
        public decimal TotalDue => (UnitPrice * Quantity) - Discount;

        public virtual SaleDto Sale { get; set; }
        public virtual ProductDto Product { get; set; }
    }
}
