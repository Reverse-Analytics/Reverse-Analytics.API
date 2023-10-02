using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities
{
    public class Sale : BaseAuditableEntity
    {
        public string Receipt { get; set; }
        public string? Comments { get; set; }
        public string? SoldBy { get; set; }
        public decimal TotalDue { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalDiscount { get; set; }
        public double TotalDiscountPercentage { get; set; }
        public DateTime SaleDate { get; set; }
        public SaleType SaleType { get; set; }
        public PaymentType PaymentType { get; set; }
        public CurrencyType Currency { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<SaleItem> SaleItems { get; set; }
        public virtual ICollection<Refund> Refunds { get; set; }
    }
}
