using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reverse_Analytics.Api.Filters;
using ReverseAnalytics.Domain.DTOs.Debt;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/debts")]
    public class DebtsController : ControllerBase
    {
        private readonly IDebtService _debtService;

        public DebtsController(IDebtService debtService)
        {
            _debtService = debtService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DebtDto>>> GetAllDebtsAsync()
        {
            var debts = await _debtService.GetAllDebtsAsync();

            return Ok(debts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DebtDto>> GetDebtByIdAsync(int id)
        {
            var debt = await _debtService.GetDebtByIdAsync(id);

            if (debt is null)
                return NotFound($"Debt with id: {id} does not exist.");

            return Ok(debt);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<DebtDto>> CreateDebtAsync([FromBody] DebtForCreateDto debtToCreate)
        {
            var createdDebt = await _debtService.CreateDebtAsync(debtToCreate);

            if (createdDebt is null)
            {
                return StatusCode(500,
                    "Something went wrong while creating new Debt. Please, try again later.");
            }

            return Ok(createdDebt);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateDebtAsync([FromBody] DebtForUpdateDto debtToUpdate, int id)
        {
            if (id != debtToUpdate.Id)
                return BadRequest($"Debt id: {debtToUpdate.Id} does not match with route id: {id}.");

            await _debtService.UpdateDebtAsync(debtToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDebtAsync(int id)
        {
            await _debtService.DeleteDebtAsync(id);

            return NoContent();
        }
    }
}
