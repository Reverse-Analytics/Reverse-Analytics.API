namespace ReverseAnalytics.Domain.Entities
{
    public class Supply : Transaction
    {
        public string? ReceivedBy { get; set; }

        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<SupplyDetail> SupplyDetails { get; set; }

        public Supply()
        {
            SupplyDetails = new List<SupplyDetail>();
        }
    }
}
