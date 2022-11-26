using ReverseAnalytics.Domain.DTOs.InventoryDetail;

namespace ReverseAnalytics.Domain.DTOs.Inventory
{
    public class InventoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<InventoryDetailDto> Details { get; set; }
    }
}
