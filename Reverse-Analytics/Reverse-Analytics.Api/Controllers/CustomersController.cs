using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.CustomerPhoneDto;
using ReverseAnalytics.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using ReverseAnalytics.Domain.DTOs.Phone;
using ReverseAnalytics.Domain.DTOs.Debt;
using ReverseAnalytics.Domain.DTOs.Address;
using Reverse_Analytics.Api.Filters;

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
        
        private const int pageSize = 15;

        public CustomersController(ICustomerService customerService, IAddressService addressService, 
            IDebtService debtService, IPhoneService phoneService)
        {
            _customerService = customerService;
            _addressService = addressService;
            _debtService = debtService;
            _phoneService = phoneService;
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

        #region Addresses

        [HttpGet("{customerId}/addresses")]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetCustomerAddresses(int customerId)
        {
            var addresses = await _addressService.GetAllByPersonIdAsync(customerId);

            return Ok(addresses);
        }

        [HttpGet("{customerId}/addresses/{addressId}")]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetCustomerAddressById(int customerId, int addressId)
        {
            var address = await _addressService.GetAddressByPersonAndAddressIdAsync(customerId, addressId);

            if (address is null)
                return NotFound($"Customer with id: {customerId} does not have an Address with id: {addressId}");

            return Ok(address);
        }

        [HttpPost("{customerId}/addresses")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<CustomerDto>> CreateCustomerAddressAsync([FromBody] AddressForCreateDto addressToCreate, int customerId)
        {
            if (addressToCreate.PersonId != customerId)
                return BadRequest($"Customer Id: {addressToCreate.PersonId} does not match with route id: {customerId}");

            var createdAddress = await _addressService.CreateAddressAsync(addressToCreate);

            if (createdAddress is null)
                return StatusCode(500,
                        $"Something went wrong while adding address number for customer with id: {customerId}. Please, try again later.");

            return Created("Address was successfully created", createdAddress);
        }

        [HttpPut("{customerId}/addresses/{addressId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateCustomerAddressAsync([FromBody] AddressForUpdateDto addressToUpdate, int customerId, int addressId)
        {
            if (addressToUpdate.Id != addressId)
                return BadRequest($"Address id: {addressToUpdate.Id}, does not match with route id: {addressId}.");

            if (addressToUpdate.PersonId != customerId)
                return BadRequest($"Customer id: {addressToUpdate.PersonId} does not match with route id: {customerId}");

            await _addressService.UpdateAddresAsync(addressToUpdate);

            return NoContent();
        }

        [HttpDelete("{customerId}/addresses/{addressId}")]
        public async Task<ActionResult> DeleteCustomerAddresssync(int customerId, int addressId)
        {
            await _addressService.DeleteAddressAsync(addressId);
            return NoContent();
        }

        #endregion

        #region Phones

        [HttpGet("{customerId}/phones")]
        public async Task<ActionResult<IEnumerable<PhoneDto>>> GetPhonesByCustomerIdAsync(int customerId)
        {
            var phones = await _phoneService.GetAllByPersonIdAsync(customerId);

            return Ok(phones);
        }

        [HttpGet("{customerId}/phones/{phoneId}")]
        public async Task<ActionResult<PhoneDto>> GetPhoneByCustomerAndPhoneIdAsync(int customerId, int phoneId)
        {
            var phone = await _phoneService.GetByPersonAndPhoneIdAsync(customerId, phoneId);

            if (phone is null)
                return Ok($"Customer with id: {customerId} does not have phone with id: {phoneId}");

            return Ok(phone);
        }

        [HttpPost("{customerId}/phones")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<PhoneDto>> CreateCustomerPhoneAsync([FromBody] PhoneForCreateDto phoneToCreate, int customerId)
        {
            if (phoneToCreate.PersonId != customerId)
                return BadRequest($"Customer Id: {phoneToCreate.PersonId} does not match with route id: {customerId}");

            var createdPhone = await _phoneService.CreatePhoneAsync(phoneToCreate);

            if (createdPhone is null)
                return StatusCode(500, 
                    $"Something went wrong while adding phone number for customer with id: {customerId}. Please, try again later.");

            return Created("Phone was successfully created.", createdPhone);
        }

        [HttpPut("{customerId}/phones/{phoneId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateCustomerPhoneAsync([FromBody] PhoneForUpdateDto phoneToUpdate, int customerId, int phoneId)
        {
            if (phoneToUpdate.Id != phoneId)
                return BadRequest($"Phone id: {phoneToUpdate.Id}, does not match with route id: {phoneId}.");

            if (phoneToUpdate.PersonId != customerId)
                return BadRequest($"Customer id: {phoneToUpdate.PersonId} does not match with route id: {customerId}");

            await _phoneService.UpdatePhoneAsync(phoneToUpdate);

            return NoContent();
        }

        [HttpDelete("{customerId}/phones/{phoneId}")]
        public async Task<ActionResult> DeleteCustomerPhoneAsync(int customerId, int phoneId)
        {
            await _phoneService.DeletePhoneAsync(phoneId);

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