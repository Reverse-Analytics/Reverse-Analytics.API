using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.Sale;
using ReverseAnalytics.Domain.DTOs.SaleDetail;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/sales")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly ISaleDetailService _saleDetailService;
        private readonly ILogger<SalesController> _logger;

        const int pageSize = 15;

        public SalesController(ISaleService service, ISaleDetailService saleDetailService, ILogger<SalesController> logger)
        {
            _saleService = service;
            _saleDetailService = saleDetailService;
            _logger = logger;
        }

        #region CRUD

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetSalesAsync(int pageSize = pageSize, int pageNumber = 1)
        {
            try
            {
                var sales = await _saleService.GetAllSalesAsync(pageSize, pageNumber);

                if (sales is null || !sales.Any())
                {
                    return Ok("There are no Sales.");
                }

                return Ok(sales);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while retrieving Sales.", ex.Message);
                return StatusCode(500, "There was an error retrieving Sales. Please, try agian later.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDto>> GetSaleByIdAsync(int id)
        {
            try
            {
                var sale = await _saleService.GetSaleByIdAsync(id);

                if (sale is null)
                {
                    return NotFound($"There is no Sale with id: {id}.");
                }

                return Ok(sale);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Sale with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Sale with id: {id}. Please, try again later.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SaleDto>> CreateSalesAsync([FromBody] SaleForCreateDto saleToCreate)
        {
            try
            {
                if (saleToCreate is null)
                {
                    return BadRequest("Sale to create cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Sale to create is not valid.");
                }

                var createdSale = await _saleService.CreateSaleAsync(saleToCreate);

                if (createdSale is null)
                {
                    return StatusCode(500, "Something went wrong while creating new Sale. Please, try again.");
                }

                return Ok(createdSale);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while creating new Sale.", ex.Message);
                return StatusCode(500, $"There was an error creating new Sale. Please, try again later.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSaleAsync([FromBody] SaleForUpdateDto saleToUpdate, int id)
        {
            try
            {
                if (saleToUpdate is null)
                {
                    return BadRequest("Sale to update cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Sale to update is not valid.");
                }

                if (saleToUpdate.Id != id)
                {
                    return BadRequest($"Route id: {saleToUpdate.Id} does not match with route id: {id}.");
                }

                await _saleService.UpdateSaleAsync(saleToUpdate);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating Sale with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error updating Sale with id: {id}. Please, try again later.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSaleAsync(int id)
        {
            try
            {
                await _saleService.DeleteSaleAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting Sale with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error deleting Sale with id: {id}. Please, try again later.");
            }
        }

        #endregion

        #region Sale Details

        [HttpGet("{id}/details")]
        public async Task<ActionResult<IEnumerable<SaleDetailDto>>> GetSaleDetailsAsync(int id)
        {
            try
            {
                var saleDetails = await _saleDetailService.GetAllSaleDetailsBySaleIdAsync(id);

                if(saleDetails is null || !saleDetails.Any())
                {
                    return Ok($"Sale with id: {id} has no details.");
                }

                return Ok(saleDetails);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error retrieving Details for Sale with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Details for Sale with id: {id}. Please, try again later.");
            }
        }

        [HttpGet("{id}/details/{saleDetailId}")]
        public async Task<ActionResult<SaleDetailDto>> GetSaleDetailByIdAsync(int id, int saleDetailId)
        {
            try
            {
                var saleDetail = await _saleDetailService.GetSaleDetailByIdAsync(saleDetailId);

                if(saleDetail is null)
                {
                    return NotFound($"There is no Sale Detail with id: {saleDetailId}.");
                }

                return Ok(saleDetail);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while retrieving Details for Sale with id: {id} and Details id: {saleDetailId}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Sale Details with id: {saleDetailId}. Please, try again later.");
            }
        }

        [HttpPost("{id}/details")]
        public async Task<ActionResult<SaleDetailDto>> CreateSaleDetail([FromBody] SaleDetailForCreateDto saleDetailToCreate)
        {
            try
            {
                if(saleDetailToCreate is null)
                {
                    return BadRequest("Sale Detail to create cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Sale Detail to create is not valid.");
                }

                var createdSaleDetail = await _saleDetailService.CreateSaleDetailAsync(saleDetailToCreate);

                return Ok(createdSaleDetail);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error while creating new Sale Detail.", ex.Message);
                return StatusCode(500, "There was an error creating new Sale Detail. Please, try again later.");
            }
        }

        [HttpPut("{id}/details/{saleDetailId}")]
        public async Task<ActionResult> UpdateSaleDetailAsync([FromBody] SaleDetailForUpdateDto saleDetailToUpdate, int saleDetailId)
        {
            try
            {
                if(saleDetailToUpdate is null)
                {
                    return BadRequest("Sale Detail to update cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Sale Detail to update is not valid.");
                }

                if(saleDetailId != saleDetailToUpdate.Id)
                {
                    return BadRequest($"Sale Detail id: {saleDetailToUpdate.Id} does not match with route id: {saleDetailId}");
                }

                await _saleDetailService.UpdateSaleDetailAsync(saleDetailToUpdate);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while updating Sale Detail with id: {saleDetailId}.", ex.Message);
                return StatusCode(500, $"There was an error updating Sale Detail with id: {saleDetailId}. Please, try again later.");
            }
        }

        [HttpDelete("{id}/details/{saleDetailId}")]
        public async Task<ActionResult> DeleteSaleDetailAsync(int saleDetailId)
        {
            try
            {
                await _saleDetailService.DeleteSaleDetailAsync(saleDetailId);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while deleting Sale Detail with id: {saleDetailId}.", ex.Message);
                return StatusCode(500, $"There was an error deleting Sale Detail with id: {saleDetailId}. Please, try again later.");
            }
        }

        #endregion
    }
}
