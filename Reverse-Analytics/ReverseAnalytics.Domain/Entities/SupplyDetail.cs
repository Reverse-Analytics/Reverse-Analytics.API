using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class SupplyDetail : BaseAuditableEntity
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? UnitPriceDiscount { get; set; }

        public int SupplyId { get; set; }
        public virtual Supply Supply { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
