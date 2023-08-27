using Microsoft.AspNetCore.Mvc;
using Reverse_Analytics.Api.Filters;
using ReverseAnalytics.Domain.DTOs.Customer;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        private const int pageSize = 15;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        #region CRUD

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomersAsync(string? searchString, int pageNumber = 0, int pageSize = 0)
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
    }
}