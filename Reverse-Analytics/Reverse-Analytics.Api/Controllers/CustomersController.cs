using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reverse_Analytics.Api.Filters;
using ReverseAnalytics.Domain.DTOs.Customer;
using ReverseAnalytics.Domain.DTOs.Debt;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IDebtService _debtService;

        private const int pageSize = 15;

        public CustomersController(ICustomerService customerService, IDebtService debtService)
        {
            _customerService = customerService;
            _debtService = debtService;
        }

        #region CRUD

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomersAsync(string? searchString, int pageNumber = 1, int pageSize = pageSize)
        {
            var customers = await _customerService.GetAllCustomerAsync(searchString, pageNumber, pageSize);

            return Ok(customers);
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);

            if (customer is null)
                return NotFound($"Customer with id: {customerId} does not exist.");

            return Ok(customer);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<CustomerDto>> CreateCustomerAsync([FromBody] CustomerForCreateDto customerToCreate)
        {
            var createdCustomer = await _customerService.CreateCustomerAsync(customerToCreate);

            if (createdCustomer is null)
                return StatusCode(500, "Something went wrong while creating new customer. Please, try again later.");

            return Created("Customer was successfully created.", createdCustomer);
        }

        [HttpPut("{customerId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateCustomerAsync([FromBody] CustomerForUpdateDto customerToUpdate, int customerId)
        {
            if (customerToUpdate.Id != customerId)
                return BadRequest($"Customer id: {customerToUpdate.Id}, does not match with route id: {customerId}.");

            await _customerService.UpdateCustomerAsync(customerToUpdate);

            return NoContent();
        }

        [HttpDelete("{customerId}")]
        public async Task<ActionResult> DeleteCustomerAsync(int customerId)
        {
            await _customerService.DeleteCustomerAsync(customerId);

            return NoContent();
        }

        #endregion

        #region Debts

        [HttpGet("{customerId}/debts")]
        public async Task<ActionResult<IEnumerable<DebtDto>>> GetCustomerDebtsAsync(int customerId)
        {
            var debts = await _debtService.GetAllDebtsByPersonIdAsync(customerId);

            return Ok(debts);
        }

        [HttpGet("{customerId}/debts/{debtId}")]
        public async Task<ActionResult<DebtDto>> GetDebtByCustomerAndDebtId(int customerId, int debtId)
        {
            var debt = await _debtService.GetByPersonAndDebtId(customerId, debtId);

            if (debt is null)
                return NotFound($"Customer with id: {customerId} does not have Debt with id: {debtId}.");

            return Ok(debt);
        }

        [HttpPost("{customerId}/debts")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<DebtDto>> CreateCustomerDebt([FromBody] DebtForCreateDto debtToCreate, int customerId)
        {
            if (debtToCreate.PersonId != customerId)
                return BadRequest($"Customer Id: {debtToCreate.PersonId} does not match with route id: {customerId}");

            var createdCustomerDebt = await _debtService.CreateDebtAsync(debtToCreate);

            if (createdCustomerDebt is null)
                return StatusCode(500,
                        "Something went wrong while creating new Customer Debt. Please, try again later.");

            return Created("Debt was successfully created.", createdCustomerDebt);
        }

        [HttpPut("{customerId}/debts/{debtId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateCustomerDebtAsync([FromBody] DebtForUpdateDto debtToUpdate, int customerId, int debtId)
        {
            if (debtId != debtToUpdate.Id)
                return BadRequest($"Debt id: {debtToUpdate.Id} does not match with route id: {debtId}.");

            if (customerId != debtToUpdate.PersonId)
                return BadRequest($"Customer id: {debtToUpdate.PersonId} does not match with route id: {customerId}.");

            await _debtService.UpdateDebtAsync(debtToUpdate);

            return NoContent();
        }

        [HttpDelete("{customerId}/debts/{debtId}")]
        public async Task<ActionResult> DeleteCustomerDebtAsync(int debtId)
        {
            await _debtService.DeleteDebtAsync(debtId);

            return NoContent();
        }

        #endregion
    }
}