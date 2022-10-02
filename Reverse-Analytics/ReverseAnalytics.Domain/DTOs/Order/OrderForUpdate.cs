using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Order
{
    public class OrderForUpdate
    {
        public int Id { get; set; }
        public decimal TotalDue { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountTotal { get; set; }
        public string? Comment { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus? Status { get; set; }

        public int CustomerId { get; set; }
    }
}
