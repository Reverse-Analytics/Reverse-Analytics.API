namespace ReverseAnalytics.Domain.DTOs.InventoryDetail
{
    public class InventoryDetailForUpdateDto
    {
        public int Id { get; set; }
        public double ProductsRemained { get; set; }
        public double EnoughForDays { get; set; }

        public int InventoryId { get; set; }
        public int ProductId { get; set; }
    }
}
