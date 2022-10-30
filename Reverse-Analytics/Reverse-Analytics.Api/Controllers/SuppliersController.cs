using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.Supplier;
using ReverseAnalytics.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reverse_Analytics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _service;
        private readonly ILogger<SuppliersController> _logger;

        public SuppliersController(ISupplierService service, ILogger<SuppliersController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetSuppliers(string? searchString)
        {
            try
            {
                var suppliers = await _service.GetAllSuppliersAsync(searchString);

                if(suppliers is null || !suppliers.Any())
                {
                    return Ok("There are no suppliers.");
                }

                return Ok(suppliers);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Errr retrieving suppliers", ex.Message);
                return StatusCode(500, "There was an error retrieving Suppliers. Please, try again later.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDto>> GetSupplierById(int id)
        {
            try
            {
                var supplier = await _service.GetSupplierByIdAsync(id);

                if(supplier is null)
                {
                    return NotFound($"Supplier with id: {id} does not exist.");
                }

                return Ok(supplier);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while retrieving Supplier with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Supplier with id: {id}. Please, try again later.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SupplierDto>> CreateSupplier(SupplierForCreateDto supplierToCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Supplier to create is not valid.");
                }

                var createdSupplier = await _service.CreateSupplierAsync(supplierToCreate);

                return Ok(createdSupplier);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error creating new Supplier.", ex.Message);
                return StatusCode(500, "There was an error creating a new Supplier. Please, try again later.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSupplier(SupplierForUpdateDto supplierToUpdate, int id)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest("Supplier to update is not valid.");
                }

                if(supplierToUpdate.Id != id)
                {
                    return BadRequest($"Route id: {id} does not match with Supplier id: {supplierToUpdate.Id}");
                }

                await _service.UpdateSupplierAsync(supplierToUpdate);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error updating supplier with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error updating Supplier with id: {id}. Please, try again later.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSupplier(int id)
        {
            try
            {
                await _service.DeleteSupplierAsync(id);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error deleting Supplier with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error deleting Supplier with id: {id}. Please, try again later.");
            }
        }
    }
}
