using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.CustomerPhoneDto;
using ReverseAnalytics.Domain.DTOs.CustomerPhone;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Services;
using ReverseAnalytics.Domain.DTOs.CustomerDebt;

namespace Reverse_Analytics.Api.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerPhoneService _customerPhoneSerivce;
        private readonly ICustomerDebtService _customerDebtService;
        private readonly ILogger<CustomersController> _logger;
        private const int pageSize = 15;

        public CustomersController(ICustomerService customerService, ICustomerPhoneService customerPhoneService, 
            ILogger<CustomersController> logger, ICustomerDebtService customerDebtService)
        {
            _customerService = customerService;
            _customerPhoneSerivce = customerPhoneService;
            _logger = logger;
            _customerDebtService = customerDebtService;
        }

        #region Customers

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomersAsync(string? searchString, int pageNumber = 1, int pageSize = pageSize)
        {
            try
            {
                var customers = await _customerService.GetAllCustomerAsync(searchString, pageNumber, pageSize);

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

        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerByIdAsync(int customerId)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(customerId);

                if (customer is null)
                {
                    _logger.LogInformation($"Customer with id: {customerId} was not found while retrieving.");
                    return NotFound($"Customer with id: {customerId} does not exist.");
                }

                return Ok(customer);
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation($"Customer with id: {customerId} was not found while retrieving.", ex.Message);
                return NotFound($"Customer with id: {customerId} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Customer with id: {customerId}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Customer with id: {customerId}. Please, try again later");
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

        [HttpPut("{customerId}")]
        public async Task<ActionResult> UpdateCustomerAsync([FromBody] CustomerForUpdateDto customerToUpdate, int customerId)
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

                if (customerToUpdate.Id != customerId)
                {
                    return BadRequest($"Customer id: {customerId}, does not match with route id: {customerId}.");
                }

                await _customerService.UpdateCustomerAsync(customerToUpdate);

                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation($"Customer with id: {customerId} was not found while updating.", ex.Message);
                return NotFound($"Customer with id: {customerId} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating Customer with id: {customerId}.", ex.Message);
                return StatusCode(500, $"There was an error updating Customer with id: {customerId}. Please, try again later");
            }
        }

        [HttpDelete("{customerId}")]
        public async Task<ActionResult> DeleteCustomerAsync(int customerId)
        {
            try
            {
                await _customerService.DeleteCustomerAsync(customerId);

                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation($"Customer with id: {customerId} was not found while deleting.", ex.Message);
                return NotFound($"Customer with id: {customerId} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting Customer with id: {customerId}.", ex.Message);
                return StatusCode(500, $"There was an error deleting Customer with id: {customerId}. Please, try again later");
            }
        }

        #endregion

        #region Customer Phones

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

        #endregion

        #region Customer Debts

        [HttpGet("{customerId}/debts")]
        public async Task<ActionResult<IEnumerable<CustomerDebtDto>>> GetCustomerDebtsAsync(int customerId)
        {
            try
            {
                var customerDebts = await _customerDebtService.GetAllByCustomerId(customerId);

                if (customerDebts is null || !customerDebts.Any())
                {
                    return Ok("This Customer does not have any Debts.");
                }

                return Ok(customerDebts);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error while retrieving Debts for Customer with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Debts for Customer with id: {customerId}. Please, try again later.");
            }
        }

        [HttpPost("{customerId}/debts")]
        public async Task<ActionResult<CustomerDebtDto>> CreateCustomerDebt([FromBody] CustomerDebtForCreate customerDebtToCreate)
        {
            try
            {
                if(customerDebtToCreate is null)
                {
                    return BadRequest("Customer Debt cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Customer Debt to create is not valid.");
                }

                var createdCustomerDebt = await _customerDebtService.CreateCustomerDebtAsync(customerDebtToCreate);

                if(createdCustomerDebt is null)
                {
                    return StatusCode(500, 
                        "Something went wrong while creating new Customer Debt. Please, try again later.");
                }

                return Ok(createdCustomerDebt);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error while creating new Customer Debt.", ex.Message);
                return StatusCode(500, "There was an error creating new Customer Debt. Please, try again later");
            }
        }

        [HttpPut("{customerId}/debts/{debtId}")]
        public async Task<ActionResult> UpdateCustomerDebtAsync([FromBody] CustomerDebtForUpdate customerDebtToUpdate, int customerId, int debtId)
        {
            try
            {
                if(customerDebtToUpdate is null)
                {
                    return BadRequest("Customer Debt to update cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest($"Customer Debt to update is not valid.");
                }

                if(debtId != customerDebtToUpdate.Id)
                {
                    return BadRequest($"Customer Debt id: {customerDebtToUpdate.Id} does not match with route id: {debtId}.");
                }

                await _customerDebtService.UpdateCustomerDebtAsync(customerDebtToUpdate);

                return NoContent();
            }
            catch(NotFoundException ex)
            {
                _logger.LogError($"Customer debt with id: {customerDebtToUpdate?.Id} was not found while updating.", ex.Message);
                return NotFound($"Customer Debt with id: {customerDebtToUpdate?.Id} does not exist.");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while updating Customer Debt with id: {customerDebtToUpdate?.Id}", ex.Message);
                return StatusCode(500, 
                    $"There was an error updating Customer Debt with id: {customerDebtToUpdate?.Id}. Please, try again later");
            }
        }

        [HttpDelete("{customerId}/debts/{debtId}")]
        public async Task<ActionResult> DeleteCustomerDebtAsync(int customerId, int debtId)
        {
            try
            {
                await _customerDebtService.DeleteCustomerDebtAsync(debtId);

                return NoContent();
            }
            catch(NotFoundException ex)
            {
                _logger.LogError($"Customer Debt with id: {debtId} was not found while deleting.", ex.Message);
                return NotFound($"Customer Debt with id: {debtId} does not exist.");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error deleting Customer Debt with id {debtId}.", ex.Message);
                return StatusCode(500, $"There was an error deleting Customer Debt with id: {debtId}. Please, try again later.");
            }
        }

        #endregion
    }
}