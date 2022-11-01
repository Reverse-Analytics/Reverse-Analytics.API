using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.Supply;
using ReverseAnalytics.Domain.DTOs.SupplyDetail;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Route("api/supplies")]
    [ApiController]
    public class SuppliesController : ControllerBase
    {
        private readonly ISupplyService _service;
        private readonly ISupplyDetailService _detailService;
        private readonly ILogger<SuppliesController> _logger;

        public SuppliesController(ISupplyService supplyService, ISupplyDetailService supplyDetailService, ILogger<SuppliesController> logger)
        {
            _service = supplyService;
            _detailService = supplyDetailService;
            _logger = logger;
        }

        #region CRUD

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplyDto>>> GetAllSuppliesAsync()
        {
            try
            {
                var supplies = await _service.GetAllSuppliesAsync();

                if (supplies is null || !supplies.Any())
                {
                    return Ok("There are no Supplies.");
                }

                return Ok(supplies);
            }
            catch (Exception ex)
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

                if (supply is null)
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
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                _logger.LogError($"Error updating Supplly with id: {supplyId}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Supply with id: {supplyId}.");
            }
        }

        #endregion

        #region Supply Details

        [HttpGet("{supplyId}/details")]
        public async Task<ActionResult<IEnumerable<SupplyDetailDto>>> GetSupplyDetailsAsync(int supplyId)
        {
            try
            {
                var supplyDetails = await _detailService.GetAllSupplyDetailsBySupplyIdAsync(supplyId);

                if(supplyDetails is null || !supplyDetails.Any())
                {
                    return Ok($"Supply with id: {supplyId} has no details.");
                }

                return Ok(supplyDetails);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while retrieving Details for Supply with id: {supplyId}.", ex.Message);
                return StatusCode(500, $"There was an error while retrieving details for Supply with id: {supplyId}.");
            }
        }

        [HttpPost("{supplyId}/details")]
        public async Task<ActionResult<SupplyDetailDto>> CreateSupplyDetailAsync([FromBody]SupplyDetailForCreateDto supplyDetailToCreate, int supplyId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Supply detail to create is not valid.");
                }

                var createdSupplyDetail = await _detailService.CreateSupplyDetailAsync(supplyDetailToCreate);

                return Ok(createdSupplyDetail);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while creating Detail for Supply with id: {supplyId}.", ex.Message);
                return StatusCode(500, $"There was an error while creating detail for Supply with id: {supplyId}.");
            }
        }

        [HttpPut("{supplyId}/details/{supplyDetailId}")]
        public async Task<ActionResult> UpdateSupplyDetailAsyc([FromBody] SupplyDetailForUpdateDto supplyDetailToUpdate, int supplyId, int supplyDetailId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Supply detail is not valid to update.");
                }

                if(supplyDetailToUpdate.Id != supplyDetailId)
                {
                    return BadRequest($"Supply detail id: {supplyDetailId} does not match with route {supplyDetailId}.");
                }

                if(supplyDetailToUpdate.SupplyId != supplyId)
                {
                    return BadRequest($"Supply id: {supplyDetailToUpdate.SupplyId} does not match with route id: {supplyId}.");
                }

                await _detailService.UpdateSupplyDetailAsync(supplyDetailToUpdate);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating Detail for Supply with id: {supplyId}.", ex.Message);
                return StatusCode(500, $"There was an error while updating detail for Supply with id: {supplyId}.");
            }
        }

        [HttpDelete("{supplyId}/details/{supplyDetailId}")]
        public async Task<ActionResult> DeleteSupplyDetailAsync(int supplyId, int supplyDetailId)
        {
            try
            {
                await _detailService.UpdateSupplyDetailAsync(supplyDetailId);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting Detail for Supply with id: {supplyId}.", ex.Message);
                return StatusCode(500, $"There was an error while deleting detail for Supply with id: {supplyId}.");
            }
        }

        #endregion
    }
}
