namespace ReverseAnalytics.Doman.DTOs.RefundItem
{
    public class RefundItemForUpdateDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int RefundId { get; set; }
        public int ProductId { get; set; }
    }
}
