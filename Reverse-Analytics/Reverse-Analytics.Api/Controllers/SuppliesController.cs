using Microsoft.AspNetCore.Mvc;
using Reverse_Analytics.Api.Filters;
using ReverseAnalytics.Domain.DTOs.SupplyDebt;
using ReverseAnalytics.Domain.DTOs.Supply;
using ReverseAnalytics.Domain.DTOs.SupplyItem;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/supplies")]
    public class SuppliesController : ControllerBase
    {
        private readonly ISupplyService _service;
        private readonly ISupplyItemservice _Itemservice;
        private readonly ISupplyDebtService _debtService;

        public SuppliesController(ISupplyService supplyService, ISupplyItemservice supplyItemservice,
            ISupplyDebtService debtService)
        {
            _service = supplyService;
            _Itemservice = supplyItemservice;
            _debtService = debtService;
        }

        #region CRUD

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplyDto>>> GetAllSuppliesAsync()
        {
            var supplies = await _service.GetAllSuppliesAsync();

            return Ok(supplies);
        }

        [HttpGet("{supplyId}")]
        public async Task<ActionResult<SupplyDto>> GetSupplyByIdAsync(int supplyId)
        {
            var supply = await _service.GetSupplyByIdAsync(supplyId);

            if (supply is null)
                return NotFound($"Supply with id: {supplyId} does not exist.");

            return Ok(supply);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<SupplyDto>> CreateSupplyAsync([FromBody] SupplyForCreateDto supplyToCreate)
        {
            var createdSupply = await _service.CreateSupplyAsync(supplyToCreate);

            if (createdSupply is null)
                return StatusCode(500,
                    "Something went wrong while creating new Supply. Please, try again later.");

            return Created("Supply was successfully created.", createdSupply);
        }

        [HttpPut("{supplyId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateSupplyAsync([FromBody] SupplyForUpdateDto supplyToUpdate, int supplyId)
        {
            if (supplyToUpdate.Id != supplyId)
                return BadRequest($"Supply id: {supplyToUpdate.Id} does not match with route id: {supplyId}.");

            await _service.UpdateSupplyAsync(supplyToUpdate);

            return NoContent();
        }

        [HttpDelete("{supplyId}")]
        public async Task<ActionResult> DeleteSupplyAsync(int supplyId)
        {
            await _service.DeleteSupplyAsync(supplyId);

            return NoContent();
        }

        #endregion

        #region Items

        [HttpGet("{supplyId}/Items")]
        public async Task<ActionResult<IEnumerable<SupplyItemDto>>> GetSupplyItemsAsync(int supplyId)
        {
            var supplyItems = await _Itemservice.GetAllSupplyItemsBySupplyIdAsync(supplyId);

            return Ok(supplyItems);
        }

        [HttpGet("{supplyId}/Items/{detailId}")]
        public async Task<ActionResult<SupplyItemDto>> GetBySupplyAndDetailIdAsync(int supplyId, int detailId)
        {
            var supplyDetail = await _Itemservice.GetBySupplyAndDetailIdAsync(supplyId, detailId);

            if (supplyDetail is null)
                return NotFound($"Supply with id: {supplyId} does not have Detail with id: {detailId}.");

            return Ok(supplyDetail);
        }

        [HttpPost("{supplyId}/Items")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<SupplyItemDto>> CreateSupplyDetailAsync([FromBody] SupplyItemForCreateDto supplyDetailToCreate, int supplyId)
        {
            if (supplyDetailToCreate.SupplyId != supplyId)
                return BadRequest($"Supply id: {supplyDetailToCreate.SupplyId} does not match with route id: {supplyId}.");

            var createdSupplyDetail = await _Itemservice.CreateSupplyDetailAsync(supplyDetailToCreate);

            if (createdSupplyDetail is null)
                return StatusCode(500,
                    "Something went wrong while creating new Supply detail. Please, try again later.");

            return Created("Supply detail was successfully created.", createdSupplyDetail);
        }

        [HttpPut("{supplyId}/Items/{supplyDetailId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateSupplyDetailAsyc([FromBody] SupplyItemForUpdateDto supplyDetailToUpdate, int supplyId, int supplyDetailId)
        {
            if (supplyDetailToUpdate.Id != supplyDetailId)
                return BadRequest($"Detail id: {supplyDetailToUpdate.Id} does not match with route {supplyDetailId}.");

            if (supplyDetailToUpdate.SupplyId != supplyId)
                return BadRequest($"Supply id: {supplyDetailToUpdate.SupplyId} does not match with route id: {supplyId}.");

            await _Itemservice.UpdateSupplyDetailAsync(supplyDetailToUpdate);

            return NoContent();
        }

        [HttpDelete("{supplyId}/Items/{supplyDetailId}")]
        public async Task<ActionResult> DeleteSupplyDetailAsync(int supplyId, int supplyDetailId)
        {
            await _Itemservice.DeleteSupplyDetailAsync(supplyDetailId);

            return NoContent();
        }

        #endregion

        #region Debts

        [HttpGet("{supplyId}/debts")]
        public async Task<ActionResult<IEnumerable<SupplyDebtDto>>> GetSupplyDebtsAsync(int supplyId)
        {
            return Ok(await _debtService.GetSupplyDebtsBySupplyIdAsync(supplyId));
        }

        [HttpGet("{supplyId}/debts/{debtId}")]
        public async Task<ActionResult<SupplyDebtDto>> GetSupplyDebtByIdAsync(int debtId)
        {
            var debt = await _debtService.GetSupplyDebtByIdAsync(debtId);

            return debt is null ?
                NotFound($"Debt with id: {debtId} does not exist") :
                Ok(debt);
        }

        [HttpPost("{supplyId}/debts")]
        public async Task<ActionResult<SupplyDebtDto>> CreateSupplyDebtAsync(int supplyId,
            [FromBody] SupplyDebtForCreateDto SupplyDebtToCreate)
        {
            if (supplyId != SupplyDebtToCreate.SupplyId)
            {
                return BadRequest($"Route id: {supplyId} does not match with Supply id: {SupplyDebtToCreate.SupplyId}");
            }

            var createdDebt = await _debtService.CreateSupplyDebtAsync(SupplyDebtToCreate);

            return Ok(createdDebt);
        }

        [HttpPut("{supplyId}/debts/{debtId}")]
        public async Task<ActionResult> UpdateSupplyDebtAsync(int supplyId, int debtId,
            [FromBody] SupplyDebtForUpdateDto SupplyDebtToUpdate)
        {
            if (supplyId != SupplyDebtToUpdate.SupplyId)
            {
                return BadRequest($"Route id: {supplyId} does not match with Supply id: {SupplyDebtToUpdate.SupplyId}");
            }

            if (debtId != SupplyDebtToUpdate.Id)
            {
                return BadRequest($"Route id: {debtId} does not match with Debt id: {SupplyDebtToUpdate.Id}");
            }

            await _debtService.UpdateSupplyDebtAsync(SupplyDebtToUpdate);

            return NoContent();
        }

        [HttpDelete("{supplyId}/debts/{debtId}")]
        public async Task<ActionResult> DeleteSupplyDebtAsync(int debtId)
        {
            await _debtService.DeleteSupplyDebtAsync(debtId);

            return NoContent();
        }

        #endregion
    }
}