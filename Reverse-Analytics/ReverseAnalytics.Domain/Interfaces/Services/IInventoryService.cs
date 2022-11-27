using ReverseAnalytics.Domain.DTOs.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface IInventoryService
    {
        public Task<IEnumerable<InventoryDto>> GetAllInventoriesAsync();
        public Task<InventoryDto> GetInventoryByIdAsync(int id);
        public Task<InventoryDto> CreateInventoryAsync(InventoryForCreateDto inventoryToCreate);
        public Task UpdateInventoryAsync(InventoryForUpdateDto inventoryToUpdate);
        public Task DeleteInventoryAsync(int id);
    }
}
