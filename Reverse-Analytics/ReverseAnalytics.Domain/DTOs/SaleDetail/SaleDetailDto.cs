using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.DTOs.Sale;

namespace ReverseAnalytics.Domain.DTOs.SaleDetail
{
    public class SaleDetailDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalDue => (UnitPrice * Quantity) - Discount;

        public int SaleId { get; set; }
        public virtual SaleDto Sale { get; set; }

        public int ProductId { get; set; }
        public virtual ProductDto Product { get; set; }
    }
}
