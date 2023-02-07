using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reverse_Analytics.Api.Filters;
using ReverseAnalytics.Domain.DTOs.Debt;
using ReverseAnalytics.Domain.DTOs.Supplier;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/suppliers")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly IDebtService _debtService;

        public SuppliersController(ISupplierService supplierService, IDebtService debtService)
        {
            _supplierService = supplierService;
            _debtService = debtService;
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

        #region Debts

        [HttpGet("{supplierId}/debts")]
        public async Task<ActionResult<IEnumerable<DebtDto>>> GetSupplierDebtsAsync(int supplierId)
        {
            var debts = await _debtService.GetAllDebtsByPersonIdAsync(supplierId);

            return Ok(debts);
        }

        [HttpGet("{supplierId}/debts/{debtId}")]
        public async Task<ActionResult<DebtDto>> GetDebtBySupplierAndDebtIdAsync(int supplierId, int debtId)
        {
            var debt = await _debtService.GetByPersonAndDebtId(supplierId, debtId);

            if (debt is null)
                return NotFound($"Supplier with id: {supplierId} does not have Debt with id: {debtId}.");

            return Ok(debt);
        }

        [HttpPost("{supplierId}/debts")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<DebtDto>> CreateSupplierDebtAsync([FromBody] DebtForCreateDto debtToCreate, int supplierId)
        {
            if (debtToCreate.PersonId != supplierId)
                return BadRequest($"Supplier Id: {debtToCreate.PersonId} does not match with route id: {supplierId}");

            var createdSupplierDebt = await _debtService.CreateDebtAsync(debtToCreate);

            if (createdSupplierDebt is null)
                return StatusCode(500,
                    "Something went wrong while creating new Supplier Debt. Please, try again later.");

            return Created("Debt was successfully created.", createdSupplierDebt);
        }

        [HttpPut("{supplierId}/debts/{debtId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateSupplierDebtAsync([FromBody] DebtForUpdateDto debtToUpdate, int supplierId, int debtId)
        {
            if (debtToUpdate.Id != debtId)
                return BadRequest($"Debt id: {debtToUpdate.Id} does not match with route id: {debtId}.");

            if (debtToUpdate.PersonId != supplierId)
                return BadRequest($"Supplier id: {debtToUpdate.PersonId} does not match with route id: {supplierId}.");

            await _debtService.UpdateDebtAsync(debtToUpdate);

            return NoContent();
        }

        [HttpDelete("{supplierId}/debts/{debtId}")]
        public async Task<ActionResult> DeleteSupplierDebtAsync(int debtId)
        {
            await _debtService.DeleteDebtAsync(debtId);

            return NoContent();
        }

        #endregion
    }
}