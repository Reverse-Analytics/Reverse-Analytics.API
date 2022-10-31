using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.Supply;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Route("api/supplies")]
    [ApiController]
    public class SuppliesController : ControllerBase
    {
        private readonly ISupplyService _service;
        private readonly ILogger<SuppliesController> _logger;

        public SuppliesController(ISupplyService service, ILogger<SuppliesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplyDto>>> GetAllSuppliesAsync()
        {
            try
            {
                var supplies = await _service.GetAllSuppliesAsync();

                if(supplies is null || !supplies.Any())
                {
                    return Ok("There are no Supplies.");
                }

                return Ok(supplies);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error retrieving Supplies.", ex.Message);
                return StatusCode(500, "There was an error retrieving Supplies.");
            }
        }

        [HttpGet("{supplierId}")]
        public async Task<ActionResult<IEnumerable<SupplyDto>>> GetAllSuppliesBySupplierIdAsync(int supplierId)
        {
            try
            {
                var supplies = await _service.GetAllSuppliesBySupplierIdAsync(supplierId);

                if (supplies is null || !supplies.Any())
                {
                    return Ok($"Supplier with id: {supplierId} does not have any Supplies.");
                }

                return Ok(supplies);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error retrieving Supplies.", ex.Message);
                return StatusCode(500, "There was an error retrieving Supplies.");
            }
        }

        [HttpGet("{supplyId}")]
        public async Task<ActionResult<SupplyDto>> GetSupplyByIdAsync(int supplyId)
        {
            try
            {
                var supply = await _service.GetSupplyByIdAsync(supplyId);

                if(supply is null)
                {
                    return NotFound($"There is no Supply with id: {supplyId}");
                }

                return Ok(supply);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving Supply with id: {supplyId}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Supply with id: {supplyId}.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SupplyDto>> CreateSupplyAsync([FromBody] SupplyForCreate supplyToCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Supply to create is not valid.");
                }

                var createdSupply = await _service.CreateSupplyAsync(supplyToCreate);

                return Ok(createdSupply);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error creating Supplies.", ex.Message);
                return StatusCode(500, "There was an error creating a new Supply.");
            }
        }

        [HttpPut("{supplyId}")]
        public async Task<ActionResult> UpdateSupplyAsync([FromBody] SupplyForUpdate supplyToUpdate, int supplyId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Supply to update is not valid.");
                }

                if (supplyToUpdate.Id != supplyId)
                {
                    return BadRequest($"Supply id: {supplyToUpdate.Id} does not match with route id: {supplyId}.");
                }

                await _service.UpdateSupplyAsync(supplyToUpdate);
                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error updating Supplly with id: {supplyId}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Supply with id: {supplyId}.");
            }
        }

        [HttpDelete("{supplyId}")]
        public async Task<ActionResult> DeleteSupplyAsync(int supplyId)
        {
            try
            {
                await _service.DeleteSupplyAsync(supplyId);
                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error updating Supplly with id: {supplyId}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Supply with id: {supplyId}.");
            }
        }
    }
}
