using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities
{
    public class Sale
    {
        public string Receipt { get; set; }
        public string? Comments { get; set; }
        public string? SoldBy { get; set; }
        public decimal TotalDue { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalDiscount { get; set; }
        public SaleType SaleType { get; set; }
        public DateTime SaleDate { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<SaleDetail> OrderDetails { get; set; }
        public virtual ICollection<SaleDebt> SaleDebts { get; set; }
        public virtual ICollection<RefundDetail> RefundDetails { get; set; }
    }
}
