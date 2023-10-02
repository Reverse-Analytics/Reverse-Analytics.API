using Microsoft.AspNetCore.Mvc;
using Reverse_Analytics.Api.Filters;
using ReverseAnalytics.Domain.DTOs.Sale;
using ReverseAnalytics.Domain.DTOs.SaleItem;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/sales/")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly ISaleItemservice _saleItemservice;

        private const int PageSize = 15;

        public SalesController(ISaleService service, ISaleItemservice saleItemservice)
        {
            _saleService = service;
            _saleItemservice = saleItemservice;
        }

        #region CRUD

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetSalesAsync(int pageSize = PageSize, int pageNumber = 1)
        {
            var sales = await _saleService.GetAllSalesAsync(pageSize, pageNumber);

            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDto>> GetSaleByIdAsync(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);

            if (sale is null)
                return NotFound($"There is no Sale with id: {id}.");

            return Ok(sale);
        }

        [HttpGet("customers/{customerId}")]
        public async Task<ActionResult<SaleDto>> GetSalesByCustomerIdAsync(int customerId)
        {
            var sales = await _saleService.GetSalesByCustomerIdAsync(customerId);

            return Ok(sales);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<SaleDto>> CreateSalesAsync([FromBody] SaleForCreateDto saleToCreate)
        {
            var createdSale = await _saleService.CreateSaleAsync(saleToCreate);

            if (createdSale is null)
                return StatusCode(500,
                    "Something went wrong while creating new Sale. Please, try again.");

            return Created("Sale was successfully created.", createdSale);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateSaleAsync([FromBody] SaleForUpdateDto saleToUpdate, int id)
        {
            if (saleToUpdate.Id != id)
                return BadRequest($"Route id: {saleToUpdate.Id} does not match with route id: {id}.");

            await _saleService.UpdateSaleAsync(saleToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSaleAsync(int id)
        {
            await _saleService.DeleteSaleAsync(id);

            return NoContent();
        }

        #endregion

        #region Items

        [HttpGet("{id}/Items")]
        public async Task<ActionResult<IEnumerable<SaleItemDto>>> GetSaleItemsAsync(int id)
        {
            var saleItems = await _saleItemservice.GetAllSaleItemsBySaleIdAsync(id);

            return Ok(saleItems);
        }

        [HttpGet("{saleId}/Items/{saleDetailId}")]
        public async Task<ActionResult<SaleItemDto>> GetSaleDetailByIdAsync(int saleId, int saleDetailId)
        {
            var saleDetail = await _saleItemservice.GetSaleDetailBySaleAndDetailIdAsync(saleId, saleDetailId);

            if (saleDetail is null)
                return NotFound($"Sale with id: {saleId} does not have Detail with id: {saleDetailId}.");

            return Ok(saleDetail);
        }

        [HttpPost("{saleId}/Items")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<SaleItemDto>> CreateSaleDetail([FromBody] SaleItemForCreateDto saleDetailToCreate, int saleId)
        {
            if (saleDetailToCreate.SaleId != saleId)
                return BadRequest($"Sale id: {saleDetailToCreate.SaleId} does not match with route id: {saleId}.");

            var createdSaleDetail = await _saleItemservice.CreateSaleDetailAsync(saleDetailToCreate);

            if (createdSaleDetail is null)
                return StatusCode(500,
                    "Something went wrong while creating new Sale. Please, try again later.");

            return Created("Sale detail was successfully created.", createdSaleDetail);
        }

        [HttpPut("{saleId}/Items/{saleDetailId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateSaleDetailAsync([FromBody] SaleItemForUpdateDto saleDetailToUpdate, int saleId, int saleDetailId)
        {
            if (saleDetailToUpdate.Id != saleId)
                return BadRequest($"Sale Detail id: {saleDetailToUpdate.Id} does not match with route id: {saleDetailId}.");

            if (saleDetailToUpdate.SaleId != saleId)
                return BadRequest($"Sale id: {saleDetailToUpdate.SaleId} does not match with route id: {saleId}.");

            await _saleItemservice.UpdateSaleDetailAsync(saleDetailToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}/Items/{saleDetailId}")]
        public async Task<ActionResult> DeleteSaleDetailAsync(int saleDetailId)
        {
            await _saleItemservice.DeleteSaleDetailAsync(saleDetailId);

            return NoContent();
        }

        #endregion

    }
}