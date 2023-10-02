using ReverseAnalytics.Domain.DTOs.SaleItem;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Sale
{
    public class SaleForUpdateDto
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public decimal TotalDue { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalDiscount { get; set; }
        public double TotalDiscountPercentage { get; set; }
        public DateTime SaleDate { get; set; }
        public SaleType SaleType { get; set; }
        public CurrencyType Currency { get; set; }
        public PaymentType PaymentType { get; set; }

        public int CustomerId { get; set; }

        public ICollection<SaleItemForUpdateDto> SaleItems { get; set; }
    }
}
