using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.CustomerPhoneDto;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using ReverseAnalytics.Domain.DTOs.Phone;
using ReverseAnalytics.Domain.DTOs.Debt;
using ReverseAnalytics.Domain.DTOs.Address;

namespace Reverse_Analytics.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IAddressService _addressService;
        private readonly IDebtService _debtService;
        private readonly IPhoneService _phoneService;
        private readonly ILogger<CustomersController> _logger;
        
        private const int pageSize = 15;

        public CustomersController(ICustomerService customerService, IAddressService addressService, IDebtService debtService, IPhoneService phoneService, ILogger<CustomersController> logger)
        {
            _customerService = customerService;
            _addressService = addressService;
            _debtService = debtService;
            _phoneService = phoneService;
            _logger = logger;
        }

        #region CRUD

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

        #region Addresses

        [HttpGet("{customerId}/addresses")]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetCustomerAddresses(int customerId)
        {
            try
            {
                var addresses = await _addressService.GetAllByPersonIdAsync(customerId);

                if (addresses is null || !addresses.Any())
                {
                    return Ok($"Customer with id: {customerId} does not have any addresses.");
                }

                return Ok(addresses);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Customer Addresses for Customer with id: {customerId}", ex.Message);
                return StatusCode(500, "There was an error retrieving Customer Addresses. Please, try again later.");
            }
        }

        [HttpGet("{customerId}/addresses/{addressId}")]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetCustomerAddressById(int customerId, int addressId)
        {
            try
            {
                var address = await _addressService.GetAddressByPersonAndAddressIdAsync(customerId, addressId);

                if(address is null)
                {
                    return NotFound($"Customer with id: {customerId} does not have an Address with id: {addressId}");
                }

                return Ok(address);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while retrieving Customer Address with customer id: {customerId} and address id: {addressId}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Customer Address with Customer id: {customerId} and Address id: {addressId}.");
            }
        }

        [HttpPost("{customerId}/addresses")]
        public async Task<ActionResult<CustomerDto>> CreateCustomerAddressAsync([FromBody] AddressForCreateDto addressToCreate, int customerId)
        {
            try
            {
                if (addressToCreate is null)
                {
                    return BadRequest("Customer Address to create cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Customer Address to create is not valid.");
                }

                if (addressToCreate.PersonId != customerId)
                {
                    return BadRequest($"Customer Id: {addressToCreate.PersonId} does not match with route id: {customerId}");
                }

                var createdAddress = await _addressService.CreateAddressAsync(addressToCreate);

                if (createdAddress is null)
                {
                    return StatusCode(500, 
                        $"Something went wrong while adding address number for customer with id: {customerId}. Please, try again later.");
                }

                return Ok(createdAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while adding address number for Customer with id: {customerId}.", ex.Message);
                return StatusCode(500, $"There was an error adding address number for Customer with id: {customerId}.");
            }
        }

        [HttpPut("{customerId}/addresses/{addressId}")]
        public async Task<ActionResult> UpdateCustomerAddressAsync([FromBody] AddressForUpdateDto addressToUpdate, int customerId, int addressId)
        {
            try
            {
                if (addressToUpdate is null)
                {
                    return BadRequest("Customer Address to update cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Customer Address to update is not valid.");
                }

                if (addressToUpdate.Id != addressId)
                {
                    return BadRequest($"Customer id: {addressToUpdate.PersonId}, does not match with route id: {addressId}.");
                }

                if (addressToUpdate.PersonId != customerId)
                {
                    return BadRequest($"Customer id: {addressToUpdate.PersonId} does not match with route id: {customerId}");
                }

                await _addressService.UpdateAddresAsync(addressToUpdate);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating address for customer with id: {customerId}.", ex.Message);
                return StatusCode(500, $"There was an error updating address number for customer with id: {customerId}. Please, try again later.");
            }
        }

        [HttpDelete("{customerId}/addresses/{addressId}")]
        public async Task<ActionResult> DeleteCustomerAddresssync(int customerId, int addressId)
        {
            try
            {
                await _addressService.DeleteAddressAsync(addressId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogError($"Customer address with customer id: {customerId}, address id: {addressId} was not found while deleting.", ex.Message);
                return NotFound($"Customer address with id: {addressId} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting phone for customer with id: {customerId} and phone id: {addressId}.", ex.Message);
                return StatusCode(500, $"There was an error deleting phone for customer with id: {customerId} and phone id: {addressId}.");
            }
        }

        #endregion

        #region Phones

        [HttpGet("{customerId}/phones")]
        public async Task<ActionResult<IEnumerable<PhoneDto>>> GetPhonesByCustomerIdAsync(int customerId)
        {
            try
            {
                var phones = await _phoneService.GetAllByPersonIdAsync(customerId);

                if (phones is null || !phones.Any())
                {
                    return Ok($"Customer with id: {customerId} does not have any phone numbers.");
                }

                return Ok(phones);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Customer Phones for Customer with id: {customerId}", ex.Message);
                return StatusCode(500, "There was an error retrieving Customer Phones. Please, try again later.");
            }
        }

        [HttpGet("{customerId}/phones/{phoneId}")]
        public async Task<ActionResult<PhoneDto>> GetPhoneByCustomerAndPhoneIdAsync(int customerId, int phoneId)
        {
            try
            {
                var phone = await _phoneService.GetByPersonAndPhoneIdAsync(customerId, phoneId);

                if(phone is null)
                {
                    return Ok($"Customer with id: {customerId} does not have phone with id: {phoneId}");
                }

                return Ok(phone);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while retrieving Phones for Customer with id: {customerId} and Phone id: {phoneId}", ex.Message);
                return StatusCode(500, "There was an error retrieving Customer Phones. Please, try again later.");
            }
        }

        [HttpPost("{customerId}/phones")]
        public async Task<ActionResult<CustomerDto>> CreateCustomerPhoneAsync([FromBody] PhoneForCreateDto phoneToCreate, int customerId)
        {
            try
            {
                if (phoneToCreate is null)
                {
                    return BadRequest("Customer Phone to create cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Customer Phone to create is not valid.");
                }

                if(phoneToCreate.PersonId != customerId)
                {
                    return BadRequest($"Customer Id: {phoneToCreate.PersonId} does not match with route id: {customerId}");
                }

                var createdPhone = await _phoneService.CreatePhoneAsync(phoneToCreate);

                if (createdPhone is null)
                {
                    return StatusCode(500, $"Something went wrong while adding phone number for customer with id: {customerId}. Please, try again later.");
                }

                return Ok(createdPhone);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while adding phone number for Customer with id: {customerId}.", ex.Message);
                return StatusCode(500, $"There was an error adding phone number for Customer with id: {customerId}.");
            }
        }

        [HttpPut("{customerId}/phones/{phoneId}")]
        public async Task<ActionResult> UpdateCustomerPhoneAsync([FromBody] PhoneForUpdateDto phoneToUpdate, int customerId, int phoneId)
        {
            try
            {
                if(phoneToUpdate is null)
                {
                    return BadRequest("Customer Phone to update cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Customer Phone to update is not valid.");
                }

                if (phoneToUpdate.Id != phoneId)
                {
                    return BadRequest($"Phone id: {phoneToUpdate.Id}, does not match with route id: {phoneId}.");
                }

                if(phoneToUpdate.PersonId != customerId)
                {
                    return BadRequest($"Customer id: {phoneToUpdate.PersonId} does not match with route id: {customerId}");
                }

                await _phoneService.UpdatePhoneAsync(phoneToUpdate);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while updating phone for customer with id: {customerId}.", ex.Message);
                return StatusCode(500, $"There was an error updating phone number for customer with id: {customerId}. Please, try again later.");
            }
        }

        [HttpDelete("{customerId}/phones/{phoneId}")]
        public async Task<ActionResult> DeleteCustomerPhoneAsync(int customerId, int phoneId)
        {
            try
            {
                await _phoneService.DeletePhoneAsync(phoneId);
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

        #region Debts

        [HttpGet("{customerId}/debts")]
        public async Task<ActionResult<IEnumerable<DebtDto>>> GetCustomerDebtsAsync(int customerId)
        {
            try
            {
                var debts = await _debtService.GetAllDebtsByPersonIdAsync(customerId);

                if (debts is null || !debts.Any())
                {
                    return Ok("This Customer does not have any Debts.");
                }

                return Ok(debts);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while retrieving Debts for Customer with id: {customerId}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Debts for Customer with id: {customerId}. Please, try again later.");
            }
        }

        [HttpGet("{customerId}/debts/{debtId}")]
        public async Task<ActionResult<DebtDto>> GetDebtByCustomerAndDebtId(int customerId, int debtId)
        {
            try
            {
                var debt = await _debtService.GetByPersonAndDebtId(customerId, debtId);

                if(debt is null)
                {
                    return NotFound($"Customer with id: {customerId} does not have Debt with id: {debtId}.");
                }

                return Ok(debt);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while retrieving Debt for Customer with id: {customerId} and Debt id: {debtId}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Debts for Customer with id: {customerId} and Debt id: {debtId}. Please, try again later.");
            }
        }

        [HttpPost("{customerId}/debts")]
        public async Task<ActionResult<DebtDto>> CreateCustomerDebt([FromBody] DebtForCreateDto debtToCreate, int customerId)
        {
            try
            {
                if(debtToCreate is null)
                {
                    return BadRequest("Customer Debt cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Customer Debt to create is not valid.");
                }

                if(debtToCreate.PersonId != customerId)
                {
                    return BadRequest($"Customer Id: {debtToCreate.PersonId} does not match with route id: {customerId}");
                }

                var createdCustomerDebt = await _debtService.CreateDebtAsync(debtToCreate);

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
        public async Task<ActionResult> UpdateCustomerDebtAsync([FromBody] DebtForUpdateDto debtToUpdate, int customerId, int debtId)
        {
            try
            {
                if(debtToUpdate is null)
                {
                    return BadRequest("Debt to update cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest($"Debt to update is not valid.");
                }

                if(debtId != debtToUpdate.Id)
                {
                    return BadRequest($"Debt id: {debtToUpdate.Id} does not match with route id: {debtId}.");
                }

                if(customerId != debtToUpdate.PersonId)
                {
                    return BadRequest($"Customer id: {debtToUpdate.PersonId} does not match with route id: {customerId}.");
                }

                await _debtService.UpdateDebtAsync(debtToUpdate);

                return NoContent();
            }
            catch(NotFoundException ex)
            {
                _logger.LogError($"Customer debt with id: {debtToUpdate?.Id} was not found while updating.", ex.Message);
                return NotFound($"Customer Debt with id: {debtToUpdate?.Id} does not exist.");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while updating Customer Debt with id: {debtToUpdate?.Id}", ex.Message);
                return StatusCode(500, 
                    $"There was an error updating Customer Debt with id: {debtToUpdate?.Id}. Please, try again later");
            }
        }

        [HttpDelete("{customerId}/debts/{debtId}")]
        public async Task<ActionResult> DeleteCustomerDebtAsync(int debtId)
        {
            try
            {
                await _debtService.DeleteDebtAsync(debtId);

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