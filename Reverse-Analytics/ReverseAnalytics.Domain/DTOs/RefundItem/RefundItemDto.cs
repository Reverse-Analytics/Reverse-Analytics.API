using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.DTOs.Refund;

namespace ReverseAnalytics.Doman.DTOs.RefundItem
{
    public class RefundItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public virtual RefundDto Refund { get; set; }
        public virtual ProductDto Product { get; set; }
    }
}
