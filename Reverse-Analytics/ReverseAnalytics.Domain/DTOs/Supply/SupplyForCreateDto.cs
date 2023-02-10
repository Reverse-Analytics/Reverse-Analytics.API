using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Supply
{
    public class SupplyForCreateDto
    {
        public string? ReceivedBy { get; set; }
        public string? Comment { get; set; }
        public DateTime? SupplyDate { get; set; }
        public decimal TotalDue { get; set; }
        public decimal TotalPaid { get; set; }
        public TransactionStatusType Status { get; set; }

        public int SupplierId { get; set; }
    }
}
