namespace ReverseAnalytics.Domain.DTOs.OrderItem
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? UnitPriceDiscount { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }
    }
}
