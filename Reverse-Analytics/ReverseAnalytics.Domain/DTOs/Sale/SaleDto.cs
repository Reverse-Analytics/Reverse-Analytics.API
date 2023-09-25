using ReverseAnalytics.Domain.DTOs.Customer;
using ReverseAnalytics.Domain.DTOs.SaleDebt;
using ReverseAnalytics.Domain.DTOs.SaleItem;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Sale
{
    public class SaleDto
    {
        public int Id { get; set; }
        public string Receipt { get; set; }
        public string? Comment { get; set; }
        public decimal TotalDue { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalDiscount { get; set; }
        public double DiscountPercentage { get; set; }
        public DateTime SaleDate { get; set; }
        public SaleType SaleType { get; set; }
        public PaymentType PaymentType { get; set; }
        public CurrencyType CurrencyType { get; set; }

        public virtual CustomerDto Customer { get; set; }
        public virtual SaleDebtDto SaleDebt { get; set; }

        public virtual ICollection<SaleItemDto> SaleDetails { get; set; }
    }
}
