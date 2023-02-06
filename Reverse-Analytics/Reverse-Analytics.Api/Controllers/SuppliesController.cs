using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reverse_Analytics.Api.Filters;
using ReverseAnalytics.Domain.DTOs.Supply;
using ReverseAnalytics.Domain.DTOs.SupplyDetail;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/supplies")]
    public class SuppliesController : ControllerBase
    {
        private readonly ISupplyService _service;
        private readonly ISupplyDetailService _detailService;

        public SuppliesController(ISupplyService supplyService, ISupplyDetailService supplyDetailService)
        {
            _service = supplyService;
            _detailService = supplyDetailService;
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

        #region Details

        [HttpGet("{supplyId}/details")]
        public async Task<ActionResult<IEnumerable<SupplyDetailDto>>> GetSupplyDetailsAsync(int supplyId)
        {
            var supplyDetails = await _detailService.GetAllSupplyDetailsBySupplyIdAsync(supplyId);

            return Ok(supplyDetails);
        }

        [HttpGet("{supplyId}/details/{detailId}")]
        public async Task<ActionResult<SupplyDetailDto>> GetBySupplyAndDetailIdAsync(int supplyId, int detailId)
        {
            var supplyDetail = await _detailService.GetBySupplyAndDetailIdAsync(supplyId, detailId);

            if (supplyDetail is null)
                return NotFound($"Supply with id: {supplyId} does not have Detail with id: {detailId}.");

            return Ok(supplyDetail);
        }

        [HttpPost("{supplyId}/details")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<SupplyDetailDto>> CreateSupplyDetailAsync([FromBody]SupplyDetailForCreateDto supplyDetailToCreate, int supplyId)
        {
            if (supplyDetailToCreate.SupplyId != supplyId)
                return BadRequest($"Supply id: {supplyDetailToCreate.SupplyId} does not match with route id: {supplyId}.");

            var createdSupplyDetail = await _detailService.CreateSupplyDetailAsync(supplyDetailToCreate);

            if (createdSupplyDetail is null)
                return StatusCode(500,
                    "Something went wrong while creating new Supply detail. Please, try again later.");

            return Created("Supply detail was successfully created.", createdSupplyDetail);
        }

        [HttpPut("{supplyId}/details/{supplyDetailId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateSupplyDetailAsyc([FromBody] SupplyDetailForUpdateDto supplyDetailToUpdate, int supplyId, int supplyDetailId)
        {
            if (supplyDetailToUpdate.Id != supplyDetailId)
                return BadRequest($"Detail id: {supplyDetailToUpdate.Id} does not match with route {supplyDetailId}.");

            if (supplyDetailToUpdate.SupplyId != supplyId)
                return BadRequest($"Supply id: {supplyDetailToUpdate.SupplyId} does not match with route id: {supplyId}.");

            await _detailService.UpdateSupplyDetailAsync(supplyDetailToUpdate);

            return NoContent();
        }

        [HttpDelete("{supplyId}/details/{supplyDetailId}")]
        public async Task<ActionResult> DeleteSupplyDetailAsync(int supplyId, int supplyDetailId)
        {
            await _detailService.DeleteSupplyDetailAsync(supplyDetailId);

            return NoContent();
        }

        #endregion
    }
}