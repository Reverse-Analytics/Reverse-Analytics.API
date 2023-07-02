using ReverseAnalytics.Domain.DTOs.Supplier;

namespace ReverseAnalytics.Domain.DTOs.Supply
{
    public class SupplyDto
    {
        public int Id { get; set; }
        public string? ReceivedBy { get; set; }
        public string? Comment { get; set; }
        public DateTime? SupplyDate { get; set; }
        public decimal TotalDue { get; set; }
        public decimal TotalPaid { get; set; }

        public int SupplierId { get; set; }
        public SupplierDto Supplier { get; set; }
    }
}
