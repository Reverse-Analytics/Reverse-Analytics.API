using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.Customer;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _service;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerService service, ILogger<CustomersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomersAsync(string? searchString)
        {
            try
            {
                var customers = await _service.GetAllCustomerAsync(searchString);

                if(customers is null || !customers.Any())
                {
                    return Ok("There are no customers.");
                }

                return Ok(customers);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error while retrieving Customers.", ex.Message);
                return StatusCode(500, "There was an error retrieving customers. Please, try again later.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = await _service.GetCustomerByIdAsync(id);

                if(customer is null)
                {
                    _logger.LogInformation($"Customer with id: {id} was not found while retrieving.");
                    return NotFound($"Customer with id: {id} does not exist.");
                }

                return Ok(customer);
            }
            catch(NotFoundException ex)
            {
                _logger.LogInformation($"Customer with id: {id} was not found while retrieving.", ex.Message);
                return NotFound($"Customer with id: {id} was not found.");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while retrieving Customer with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Customer with id: {id}. Please, try again later");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomerAsync([FromBody]CustomerForCreateDto customerToCreate)
        {
            try
            {
                if(customerToCreate is null)
                {
                    return BadRequest("Customer to create cannt be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Customer to create is not valid.");
                }

                var createdCustomer = await _service.CreateCustomerAsync(customerToCreate);

                if(createdCustomer is null)
                {
                    return StatusCode(500, "Something went wrong while creating new customer. Please, try again later.");
                }

                return Ok(createdCustomer);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error while creating a new Customer.", ex.Message);
                return StatusCode(500, "There was an error creating new Customer. Please, try again later");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomerAsync(int id, [FromBody]CustomerForUpdateDto customerToUpdate)
        {
            try
            {
                if(customerToUpdate is null)
                {
                    return BadRequest("Customer to update cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Customer is not valid for update");
                }

                if (customerToUpdate.Id != id)
                {
                    return BadRequest($"Customer id: {id}, does not match with route id: {id}.");
                }

                await _service.UpdateCustomerAsync(customerToUpdate);

                return NoContent();
            }
            catch(NotFoundException ex)
            {
                _logger.LogInformation($"Customer with id: {id} was not found while updating.", ex.Message);
                return NotFound($"Customer with id: {id} was not found.");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while updating Customer with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error updating Customer with id: {id}. Please, try again later");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomerAsync(int id)
        {
            try
            {
                await _service.DeleteCustomerAsync(id);

                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation($"Customer with id: {id} was not found while deleting.", ex.Message);
                return NotFound($"Customer with id: {id} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting Customer with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error deleting Customer with id: {id}. Please, try again later");
            }
        }
    }
}
