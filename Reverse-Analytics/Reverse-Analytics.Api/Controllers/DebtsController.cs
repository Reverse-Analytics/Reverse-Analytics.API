using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.Debt;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Route("api/debts")]
    [ApiController]
    public class DebtsController : ControllerBase
    {
        private readonly ILogger<DebtsController> _logger;
        private readonly IDebtService _debtService;

        public DebtsController(ILogger<DebtsController> logger, IDebtService debtService)
        {
            _logger = logger;
            _debtService = debtService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DebtDto>>> GetAllDebtsAsync()
        {
            try
            {
                var debts = await _debtService.GetAllDebtsAsync();

                if (debts is null || !debts.Any())
                {
                    return Ok("There are no Debts.");
                }

                return Ok(debts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Debts.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Debts. Please, try again later.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DebtDto>> GetDebtByIdAsync(int id)
        {
            try
            {
                var debt = await _debtService.GetDebtByIdAsync(id);

                if (debt is null)
                {
                    return NotFound($"Therer is no Debt with id: {id}.");
                }

                return Ok(debt);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Debt with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Debt with id: {id}. Please, try again later.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<DebtDto>> CreateDebtAsync([FromBody] DebtForCreateDto debtToCreate)
        {
            try
            {
                if (debtToCreate is null)
                {
                    return BadRequest("Debt cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Debt to create is not valid.");
                }

                var createdDebt = await _debtService.CreateDebtAsync(debtToCreate);

                if (createdDebt is null)
                {
                    return StatusCode(500,
                        "Something went wrong while creating new Debt. Please, try again later.");
                }

                return Ok(createdDebt);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while creating new Debt.", ex.Message);
                return StatusCode(500, "There was an error creating new Debt. Please, try again later");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDebtAsync([FromBody] DebtForUpdateDto debtToUpdate, int id)
        {
            try
            {
                if (debtToUpdate is null)
                {
                    return BadRequest("Debt to update cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest($"Debt to update is not valid.");
                }

                if (id != debtToUpdate.Id)
                {
                    return BadRequest($"Debt id: {debtToUpdate.Id} does not match with route id: {id}.");
                }

                await _debtService.UpdateDebtAsync(debtToUpdate);

                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogError($"Debt with id: {debtToUpdate?.Id} was not found while updating.", ex.Message);
                return NotFound($"Debt with id: {debtToUpdate?.Id} does not exist.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating Debt with id: {debtToUpdate?.Id}", ex.Message);
                return StatusCode(500,
                    $"There was an error updating Debt with id: {debtToUpdate?.Id}. Please, try again later");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDebtAsync(int id)
        {
            try
            {
                await _debtService.DeleteDebtAsync(id);

                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogError($"Debt with id: {id} was not found while deleting.", ex.Message);
                return NotFound($"Debt with id: {id} does not exist.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting Debt with id {id}.", ex.Message);
                return StatusCode(500, $"There was an error deleting Debt with id: {id}. Please, try again later.");
            }
        }
    }
}
