using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.DTOs.Refund;

namespace ReverseAnalytics.Domain.DTOs.RefundDetail
{
    public class RefundDetailDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public virtual RefundDto Refund { get; set; }
        public virtual ProductDto Product { get; set; }
    }
}
