using Microsoft.AspNetCore.Mvc;
using Reverse_Analytics.Api.Filters;
using ReverseAnalytics.Domain.DTOs.Supplier;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/suppliers")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        #region CRUD

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetSuppliersAsync(string? searchString)
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync(searchString);

            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDto>> GetSupplierByIdAsync(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);

            if (supplier is null)
                return NotFound($"Supplier with id: {id} does not exist.");

            return Ok(supplier);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<SupplierDto>> CreateSupplierAsync([FromBody] SupplierForCreateDto supplierToCreate)
        {
            var createdSupplier = await _supplierService.CreateSupplierAsync(supplierToCreate);

            if (createdSupplier is null)
                return StatusCode(500, "Something went wrong while creating new Supplier. Please, try again later.");

            return Created("Supplier was successfully created.", createdSupplier);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateSupplierAsync([FromBody] SupplierForUpdateDto supplierToUpdate, int id)
        {
            if (supplierToUpdate.Id != id)
                return BadRequest($"Route id: {supplierToUpdate.Id} does not match with Supplier id: {id}");

            await _supplierService.UpdateSupplierAsync(supplierToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSupplierAsync(int id)
        {
            await _supplierService.DeleteSupplierAsync(id);

            return NoContent();
        }

        #endregion
    }
}