namespace ReverseAnalytics.Domain.DTOs.RefundDetail
{
    public class RefundDetailForCreateDto
    {
        public int Quantity { get; set; }
        public int RefundId { get; set; }
        public int ProductId { get; set; }
    }
}
