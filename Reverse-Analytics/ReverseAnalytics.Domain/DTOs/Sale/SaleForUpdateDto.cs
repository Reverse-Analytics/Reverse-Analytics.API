using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Sale
{
    public class SaleForUpdateDto
    {
        public int Id { get; set; }
        public string Receipt { get; set; }
        public string? Comment { get; set; }
        public decimal TotalDue { get; set; }
        public decimal TotalPaid { get; set; }
        public double? DiscountPercentage { get; set; }
        public decimal? DiscountTotal { get; set; }
        public DateTime SaleDate { get; set; }
        public SaleType SaleType { get; set; }
        public TransactionStatus Status { get; set; }

        public int CustomerId { get; set; }
    }
}
