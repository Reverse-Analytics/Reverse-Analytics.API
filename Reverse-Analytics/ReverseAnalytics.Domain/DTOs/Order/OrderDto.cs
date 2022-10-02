using ReverseAnalytics.Domain.DTOs.CustomerPhoneDto;
using ReverseAnalytics.Domain.DTOs.OrderItem;
using ReverseAnalytics.Domain.DTOs.OrderItem;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public decimal TotalDue { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountTotal { get; set; }
        public string? Comment { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus? Status { get; set; }

        public int CustomerId { get; set; }

        public ICollection<OrderItemDto> OrderItems { get; set; }

    }
}
