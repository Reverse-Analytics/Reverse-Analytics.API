using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reverse_Analytics.Api.Filters;
using ReverseAnalytics.Domain.DTOs.Address;
using ReverseAnalytics.Domain.DTOs.Debt;
using ReverseAnalytics.Domain.DTOs.Phone;
using ReverseAnalytics.Domain.DTOs.Supplier;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/suppliers")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly IAddressService _addressService;
        private readonly IPhoneService _phoneService;
        private readonly IDebtService _debtService;

        public SuppliersController(ISupplierService supplierService, IAddressService addressService, 
            IPhoneService phoneService, IDebtService debtService)
        {
            _supplierService = supplierService;
            _addressService = addressService;
            _phoneService = phoneService;
            _debtService = debtService;
        }

        #region CRUD

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetSuppliersAsync(string? searchString)
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync(searchString);

            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDto>> GetSupplierByIdAsync(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);

            if (supplier is null)
                return NotFound($"Supplier with id: {id} does not exist.");

            return Ok(supplier);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<SupplierDto>> CreateSupplierAsync([FromBody] SupplierForCreateDto supplierToCreate)
        {
            var createdSupplier = await _supplierService.CreateSupplierAsync(supplierToCreate);

            if (createdSupplier is null)
                return StatusCode(500, "Something went wrong while creating new Supplier. Please, try again later.");

            return Created("Supplier was successfully created.", createdSupplier);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateSupplierAsync([FromBody] SupplierForUpdateDto supplierToUpdate, int id)
        {
            if (supplierToUpdate.Id != id)
                return BadRequest($"Route id: {supplierToUpdate.Id} does not match with Supplier id: {id}");

            await _supplierService.UpdateSupplierAsync(supplierToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSupplierAsync(int id)
        {
            await _supplierService.DeleteSupplierAsync(id);

            return NoContent();
        }

        #endregion

        #region Addresses

        [HttpGet("{supplierId}/addresses")]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetSupplierAddressesAsync(int supplierId)
        {
            var addresses = await _addressService.GetAllByPersonIdAsync(supplierId);

            return Ok(addresses);
        }

        [HttpGet("{supplierId}/addresses/{addressId}")]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetSupplierAddressByIdAsync(int supplierId, int addressId)
        {
            var address = await _addressService.GetAddressByPersonAndAddressIdAsync(supplierId, addressId);

            if (address is null)
                return NotFound($"Supplier with id: {supplierId} does not have an Address with id: {addressId}");

            return Ok(address);
        }

        [HttpPost("{supplierId}/addresses")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<AddressDto>> CreateSupplierAddressAsync([FromBody] AddressForCreateDto addressToCreate, int supplierId)
        {
            if (addressToCreate.PersonId != supplierId)
                return BadRequest($"Supplier Id: {addressToCreate.PersonId} does not match with route id: {supplierId}");

            var createdAddress = await _addressService.CreateAddressAsync(addressToCreate);

            if (createdAddress is null)
                return StatusCode(500,
                    $"Something went wrong while adding address number for Supplier with id: {supplierId}. Please, try again later.");

            return Created("Address was successfully created.", createdAddress);
        }

        [HttpPut("{supplierId}/addresses/{addressId}")]
        public async Task<ActionResult> UpdateSupplierAddressAsync([FromBody] AddressForUpdateDto addressToUpdate, int supplierId, int addressId)
        {
            if (addressToUpdate.Id != addressId)
                return BadRequest($"Address id: {addressToUpdate.Id}, does not match with route id: {addressId}.");

            if (addressToUpdate.PersonId != supplierId)
                return BadRequest($"Supplier id: {addressToUpdate.PersonId} does not match with route id: {supplierId}");

            await _addressService.UpdateAddresAsync(addressToUpdate);

            return NoContent();
        }

        [HttpDelete("{supplierId}/addresses/{addressId}")]
        public async Task<ActionResult> DeleteSupplierAddresssync(int supplierId, int addressId)
        {
            await _addressService.DeleteAddressAsync(addressId);

            return NoContent();
        }

        #endregion

        #region Phones

        [HttpGet("{supplierId}/phones")]
        public async Task<ActionResult<IEnumerable<PhoneDto>>> GetPhonesBysupplierIdAsync(int supplierId)
        {
            var phones = await _phoneService.GetAllByPersonIdAsync(supplierId);

            return Ok(phones);
        }

        [HttpGet("{supplierId}/phones/{phoneId}")]
        public async Task<ActionResult<PhoneDto>> GetPhoneBySupplierAndPhoneIdAsync(int supplierId, int phoneId)
        {
            var phone = await _phoneService.GetByPersonAndPhoneIdAsync(supplierId, phoneId);

            if (phone is null)
                return NotFound($"Supplier with id: {supplierId} does not have phone with id: {phoneId}");

            return Ok(phone);
        }

        [HttpPost("{supplierId}/phones")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<PhoneDto>> CreateSupplierPhoneAsync([FromBody] PhoneForCreateDto phoneToCreate, int supplierId)
        {
            if (phoneToCreate.PersonId != supplierId)
                return BadRequest($"Supplier Id: {phoneToCreate.PersonId} does not match with route id: {supplierId}");

            var createdPhone = await _phoneService.CreatePhoneAsync(phoneToCreate);

            if (createdPhone is null)
                return StatusCode(500, $"Something went wrong while adding phone number for Supplier with id: {supplierId}. Please, try again later.");

            return Created("Phone was successfully created.", createdPhone);
        }

        [HttpPut("{supplierId}/phones/{phoneId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateSupplierPhoneAsync([FromBody] PhoneForUpdateDto phoneToUpdate, int supplierId, int phoneId)
        {
            if (phoneToUpdate.Id != phoneId)
                return BadRequest($"Phone id: {phoneToUpdate.Id}, does not match with route id: {phoneId}.");

            if (phoneToUpdate.PersonId != supplierId)
                return BadRequest($"Supplier id: {phoneToUpdate.PersonId} does not match with route id: {supplierId}");

            await _phoneService.UpdatePhoneAsync(phoneToUpdate);

            return NoContent();
        }

        [HttpDelete("{supplierId}/phones/{phoneId}")]
        public async Task<ActionResult> DeleteSupplierPhoneAsync(int supplierId, int phoneId)
        {
            await _phoneService.DeletePhoneAsync(phoneId);

            return NoContent();
        }

        #endregion

        #region Debts

        [HttpGet("{supplierId}/debts")]
        public async Task<ActionResult<IEnumerable<DebtDto>>> GetSupplierDebtsAsync(int supplierId)
        {
            var debts = await _debtService.GetAllDebtsByPersonIdAsync(supplierId);

            return Ok(debts);
        }

        [HttpGet("{supplierId}/debts/{debtId}")]
        public async Task<ActionResult<DebtDto>> GetDebtBySupplierAndDebtIdAsync(int supplierId, int debtId)
        {
            var debt = await _debtService.GetByPersonAndDebtId(supplierId, debtId);

            if (debt is null)
                return NotFound($"Supplier with id: {supplierId} does not have Debt with id: {debtId}.");

            return Ok(debt);
        }

        [HttpPost("{supplierId}/debts")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<DebtDto>> CreateSupplierDebtAsync([FromBody] DebtForCreateDto debtToCreate, int supplierId)
        {
            if (debtToCreate.PersonId != supplierId)
                return BadRequest($"Supplier Id: {debtToCreate.PersonId} does not match with route id: {supplierId}");

            var createdSupplierDebt = await _debtService.CreateDebtAsync(debtToCreate);

            if (createdSupplierDebt is null)
                return StatusCode(500,
                    "Something went wrong while creating new Supplier Debt. Please, try again later.");

            return Created("Debt was successfully created.", createdSupplierDebt);
        }

        [HttpPut("{supplierId}/debts/{debtId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateSupplierDebtAsync([FromBody] DebtForUpdateDto debtToUpdate, int supplierId, int debtId)
        {
            if (debtToUpdate.Id != debtId)
                return BadRequest($"Debt id: {debtToUpdate.Id} does not match with route id: {debtId}.");

            if (debtToUpdate.PersonId != supplierId)
                return BadRequest($"Supplier id: {debtToUpdate.PersonId} does not match with route id: {supplierId}.");

            await _debtService.UpdateDebtAsync(debtToUpdate);

            return NoContent();
        }

        [HttpDelete("{supplierId}/debts/{debtId}")]
        public async Task<ActionResult> DeleteSupplierDebtAsync(int debtId)
        {
            await _debtService.DeleteDebtAsync(debtId);

            return NoContent();
        }

        #endregion
    }
}