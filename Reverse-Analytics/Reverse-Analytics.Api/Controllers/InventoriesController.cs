using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.Inventory;
using ReverseAnalytics.Domain.DTOs.InventoryDetail;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly IInventoryDetailService _inventoryDetailService;
        private readonly ILogger<InventoriesController> _logger;

        public InventoriesController(IInventoryService inventoryService, IInventoryDetailService inventoryDetailService, ILogger<InventoriesController> logger)
        {
            _inventoryService = inventoryService;
            _inventoryDetailService = inventoryDetailService;
            _logger = logger;
        }

        #region CRUD

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryDto>>> GetAllInventoriesAsync()
        {
            try
            {
                var inventories = await _inventoryService.GetAllInventoriesAsync();

                if (inventories is null || !inventories.Any())
                {
                    return Ok("There are no Inventories.");
                }

                return Ok(inventories);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while retrieving Inventories.", ex.Message);
                return StatusCode(500, "There was an error retrieving Inventories. Please, try agian later.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryDto>> GetInventoryByIdAsync(int id)
        {
            try
            {
                var inventory = await _inventoryService.GetInventoryByIdAsync(id);

                if (inventory is null)
                {
                    return NotFound($"There is no Inventory with id: {id}.");
                }

                return Ok(inventory);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Inventory with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Inventory with id: {id}. Please, try again later.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<InventoryDto>> CreateInventorysAsync([FromBody] InventoryForCreateDto inventoryToCreate)
        {
            try
            {
                if (inventoryToCreate is null)
                {
                    return BadRequest("Inventory to create cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Inventory to create is not valid.");
                }

                var createdInventory = await _inventoryService.CreateInventoryAsync(inventoryToCreate);

                if (createdInventory is null)
                {
                    return StatusCode(500, "Something went wrong while creating new Inventory. Please, try again.");
                }

                return Ok(createdInventory);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while creating new Inventory.", ex.Message);
                return StatusCode(500, $"There was an error creating new Inventory. Please, try again later.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateInventoryAsync([FromBody] InventoryForUpdateDto inventoryToUpdate, int id)
        {
            try
            {
                if (inventoryToUpdate is null)
                {
                    return BadRequest("Inventory to update cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Inventory to update is not valid.");
                }

                if (inventoryToUpdate.Id != id)
                {
                    return BadRequest($"Route id: {inventoryToUpdate.Id} does not match with route id: {id}.");
                }

                await _inventoryService.UpdateInventoryAsync(inventoryToUpdate);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating Inventory with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error updating Inventory with id: {id}. Please, try again later.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInventoryAsync(int id)
        {
            try
            {
                await _inventoryService.DeleteInventoryAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting Inventory with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error deleting Inventory with id: {id}. Please, try again later.");
            }
        }

        #endregion

        #region Details

        [HttpGet("{id}/details")]
        public async Task<ActionResult<IEnumerable<InventoryDetailDto>>> GetInventoryDetailsAsync(int id)
        {
            try
            {
                var inventoryDetails = await _inventoryDetailService.GetAllByInventoryIdAsync(id);

                if (inventoryDetails is null || !inventoryDetails.Any())
                {
                    return Ok($"Inventory with id: {id} has no details.");
                }

                return Ok(inventoryDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving Details for Inventory with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Details for Inventory with id: {id}. Please, try again later.");
            }
        }

        [HttpGet("{inventoryId}/details/{inventoryDetailId}")]
        public async Task<ActionResult<InventoryDetailDto>> GetInventoryDetailByIdAsync(int inventoryId, int inventoryDetailId)
        {
            try
            {
                var inventoryDetail = await _inventoryDetailService.GetByInventoryAndDetailIdAsync(inventoryId, inventoryDetailId);

                if (inventoryDetail is null)
                {
                    return NotFound($"Inventory with id: {inventoryId} does not have Detail with id: {inventoryDetailId}.");
                }

                return Ok(inventoryDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Details for Inventory with id: {inventoryId} and Details id: {inventoryDetailId}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Inventory Details with id: {inventoryDetailId}. Please, try again later.");
            }
        }

        [HttpPost("{inventoryId}/details")]
        public async Task<ActionResult<InventoryDetailDto>> CreateInventoryDetailAsync([FromBody] InventoryDetailForCreateDto inventoryDetailToCreate, int inventoryId)
        {
            try
            {
                if (inventoryDetailToCreate is null)
                {
                    return BadRequest("Inventory Detail to create cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Inventory Detail to create is not valid.");
                }

                if (inventoryDetailToCreate.InventoryId != inventoryId)
                {
                    return BadRequest($"Inventory id: {inventoryDetailToCreate.InventoryId} does not match with route id: {inventoryId}.");
                }

                var createdInventoryDetail = await _inventoryDetailService.CreateInventoryDetailAsync(inventoryDetailToCreate);

                return Ok(createdInventoryDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while creating new Inventory Detail.", ex.Message);
                return StatusCode(500, "There was an error creating new Inventory Detail. Please, try again later.");
            }
        }

        [HttpPut("{InventoryId}/details/{inventoryDetailId}")]
        public async Task<ActionResult> UpdateInventoryDetailAsync([FromBody] InventoryDetailForUpdateDto inventoryDetailToUpdate, int inventoryId, int inventoryDetailId)
        {
            try
            {
                if (inventoryDetailToUpdate is null)
                {
                    return BadRequest("Inventory Detail to update cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Inventory Detail to update is not valid.");
                }

                if (inventoryDetailId != inventoryDetailToUpdate.Id)
                {
                    return BadRequest($"Inventory Detail id: {inventoryDetailToUpdate.Id} does not match with route id: {inventoryDetailId}");
                }

                if (inventoryDetailToUpdate.InventoryId != inventoryId)
                {
                    return BadRequest($"Inventory id: {inventoryDetailToUpdate.InventoryId} does not match with route id: {inventoryId}.");
                }

                await _inventoryDetailService.UpdateInventoryDetailAsync(inventoryDetailToUpdate);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating Inventory Detail with id: {inventoryDetailId}.", ex.Message);
                return StatusCode(500, $"There was an error updating Inventory Detail with id: {inventoryDetailId}. Please, try again later.");
            }
        }

        [HttpDelete("{id}/details/{inventoryDetailId}")]
        public async Task<ActionResult> DeleteInventoryDetailAsync(int inventoryDetailId)
        {
            try
            {
                await _inventoryDetailService.DeleteInventoryDetailAsync(inventoryDetailId);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting Inventory Detail with id: {inventoryDetailId}.", ex.Message);
                return StatusCode(500, $"There was an error deleting Inventory Detail with id: {inventoryDetailId}. Please, try again later.");
            }
        }

        #endregion
    }
}
