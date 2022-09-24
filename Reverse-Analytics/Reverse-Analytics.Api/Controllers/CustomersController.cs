using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.Customer;
using ReverseAnalytics.Domain.DTOs.CustomerPhone;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerPhoneService _customerPhoneSerivce;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerService customerService, ICustomerPhoneService customerPhoneService, ILogger<CustomersController> logger)
        {
            _customerService = customerService;
            _customerPhoneSerivce = customerPhoneService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomersAsync(string? searchString)
        {
            try
            {
                var customers = await _customerService.GetAllCustomerAsync(searchString);

                if (customers is null || !customers.Any())
                {
                    return Ok("There are no customers.");
                }

                return Ok(customers);
            }
            catch (Exception ex)
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
                var customer = await _customerService.GetCustomerByIdAsync(id);

                if (customer is null)
                {
                    _logger.LogInformation($"Customer with id: {id} was not found while retrieving.");
                    return NotFound($"Customer with id: {id} does not exist.");
                }

                return Ok(customer);
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation($"Customer with id: {id} was not found while retrieving.", ex.Message);
                return NotFound($"Customer with id: {id} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Customer with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Customer with id: {id}. Please, try again later");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomerAsync([FromBody] CustomerForCreateDto customerToCreate)
        {
            try
            {
                if (customerToCreate is null)
                {
                    return BadRequest("Customer to create cannt be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Customer to create is not valid.");
                }

                var createdCustomer = await _customerService.CreateCustomerAsync(customerToCreate);

                if (createdCustomer is null)
                {
                    return StatusCode(500, "Something went wrong while creating new customer. Please, try again later.");
                }

                return Ok(createdCustomer);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while creating a new Customer.", ex.Message);
                return StatusCode(500, "There was an error creating new Customer. Please, try again later");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomerAsync(int id, [FromBody] CustomerForUpdateDto customerToUpdate)
        {
            try
            {
                if (customerToUpdate is null)
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

                await _customerService.UpdateCustomerAsync(customerToUpdate);

                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation($"Customer with id: {id} was not found while updating.", ex.Message);
                return NotFound($"Customer with id: {id} was not found.");
            }
            catch (Exception ex)
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
                await _customerService.DeleteCustomerAsync(id);

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

        [HttpGet("{customerId}/customerPhones")]
        public async Task<ActionResult<IEnumerable<CustomerPhoneDto>>> GetCustomerPhonesAsync(int customerId)
        {
            try
            {
                var customerPhones = await _customerPhoneSerivce.GetCustomerPhonesByCustomerIdAsync(customerId);

                if (customerPhones is null || !customerPhones.Any())
                {
                    return Ok($"Customer with id: {customerId} does not have any phone numbers.");
                }

                return Ok(customerPhones);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Customer Phones for Customer with id: {customerId}", ex.Message);
                return StatusCode(500, "There was an error retrieving Customer Phones. Please, try again later.");
            }
        }

        [HttpPost("{customerId}/customerphones")]
        public async Task<ActionResult<CustomerDto>> CreateCustomerPhoneAsync([FromBody] CustomerPhoneForCreate customerPhoneToCreate, int customerId)
        {
            try
            {
                if (customerPhoneToCreate is null)
                {
                    return BadRequest("Customer Phone to create cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Customer Phone to create is not valid.");
                }

                var customerPhoneDto = await _customerPhoneSerivce.CreateCustomerPhoneAsync(customerPhoneToCreate);

                if (customerPhoneToCreate is null)
                {
                    return StatusCode(500, $"Something went wrong while adding phone number for customer with id: {customerId}. Please, try again later.");
                }

                return Ok(customerPhoneDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while adding phone number for Customer with id: {customerId}.", ex.Message);
                return StatusCode(500, $"There was an error adding phone number for Customer with id: {customerId}.");
            }
        }

        [HttpPut("{customerId}/customerPhones/{phoneId}")]
        public async Task<ActionResult> UpdateCustomerPhoneAsync([FromBody] CustomerPhoneForUpdate customerPhoneToUpdate, int customerId, int phoneId)
        {
            try
            {
                if(customerPhoneToUpdate is null)
                {
                    return BadRequest("Customer Phone to update cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Customer Phone to update is not valid.");
                }

                if (customerPhoneToUpdate.Id != phoneId)
                {
                    return BadRequest($"Customer id: {phoneId}, does not match with route id: {phoneId}.");
                }

                await _customerPhoneSerivce.UpdateCustomerPhoneAsync(customerPhoneToUpdate);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while updating phone for customer with id: {customerId}.", ex.Message);
                return StatusCode(500, $"There was an error updating phone number for customer with id: {customerId}. Please, try again later.");
            }
        }

        [HttpDelete("{customerId}/customerPhones/{phoneId}")]
        public async Task<ActionResult> DeleteCustomerPhoneAsync(int customerId, int phoneId)
        {
            try
            {
                await _customerPhoneSerivce.DeleteCustomerPhoneAsync(phoneId);
                return NoContent();
            }
            catch(NotFoundException ex)
            {
                _logger.LogError($"Customer phone with customer id: {customerId}, phone id: {phoneId} was not found while deleting.", ex.Message);
                return NotFound($"Customer phone with id: {phoneId} was not found.");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while deleting phone for customer with id: {customerId} and phone id: {phoneId}.", ex.Message);
                return StatusCode(500, $"There was an error deleting phone for customer with id: {customerId} and phone id: {phoneId}.");
            }
        }
    }
}