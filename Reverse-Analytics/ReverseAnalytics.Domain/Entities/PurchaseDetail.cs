using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class PurchaseDetail : BaseAuditableEntity
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? UnitPriceDiscount { get; set; }

        public int PurchaseId { get; set; }
        public virtual Purchase Purchase { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
