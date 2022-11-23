using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.CustomerDebt;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/customerdebts")]
    public class CustomerDebtsController : ControllerBase
    {
        private readonly ICustomerDebtService _service;
        private readonly ILogger<CustomerDebtsController> _logger;

        public CustomerDebtsController(ICustomerDebtService service, ILogger<CustomerDebtsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDebtDto>>> GetCustomerDebtsAsync()
        {
            try
            {
                var customerDebts = await _service.GetAllCustomerDebtsAsync();

                if (customerDebts is null || !customerDebts.Any())
                {
                    return Ok("There are no Customer Debts.");
                }

                return Ok(customerDebts);
            }
            catch(AutoMapperMappingException ex)
            {
                _logger.LogError($"Error mapping Customer Debts", ex.Message);
                return StatusCode(500, "There was an internal error. Please, try again later.");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error retrieving Customer Debts.", ex.Message);
                return StatusCode(500, "There was an internal error. Please, try again later.");
            }
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<CustomerDebtDto>> GetCustomerDebtByIdAsync(int id)
        {
            try
            {
                var customerDebt = await _service.GetCustomerDebtByIdAsync(id);

                if (customerDebt is null)
                {
                    return NotFound($"There is no Customer Debt with id: {id}.");
                }

                return Ok(customerDebt);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Errr while retrieving Customer Debt with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Customer Debt with id: {id}. Please, try again later.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDebtDto>> CreateCustomerDebtAsync([FromBody] CustomerDebtForCreate customerDebtToCreate)
        {
            try
            {
                if (customerDebtToCreate is null)
                {
                    return BadRequest("Customer Debt to create cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Customer Debt to create is not valid.");
                }

                var customerDebtDto = await _service.CreateCustomerDebtAsync(customerDebtToCreate);

                if(customerDebtDto is null)
                {
                    return StatusCode(500, "Something went wrong while creating new Customer Debt. Please, try again later.");
                }

                return Ok(customerDebtDto);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error while creating new Customer Debt.", ex.Message);
                return StatusCode(500, "There was an error creating new Customer Debt. Please, try again later.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomerDebtAsync([FromBody] CustomerDebtForUpdate customerDebtToUpdate, int id)
        {
            try
            {
                if(customerDebtToUpdate is null)
                {
                    return BadRequest("Customer Debt to update cannot be null.");
                }

                if (customerDebtToUpdate.Id != id)
                {
                    return BadRequest($"Customer Debt id: {customerDebtToUpdate.Id} does not match with URL id: {id}.");
                }

                await _service.UpdateCustomerDebtAsync(customerDebtToUpdate);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error updating Customer Debt with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error updating Customer Debt with id: {id}. Please, try again later.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomerDebtAsync(int id)
        {
            try
            {
                await _service.DeleteCustomerDebtAsync(id);

                return NoContent();
            }
            catch(NotFoundException ex)
            {
                _logger.LogInformation($"Customer Debt with id: {id} while deleting.", ex.Message);
                return NotFound($"Customer Debt with id: {id} was not found.");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error deleting Customer Debt with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error deleting Customer Debt with id: {id}. Please, try again later.");
            }
        }
    }
}
