using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.Address;
using ReverseAnalytics.Domain.DTOs.Debt;
using ReverseAnalytics.Domain.DTOs.Phone;
using ReverseAnalytics.Domain.DTOs.Supplier;
using ReverseAnalytics.Domain.DTOs.Supply;
using ReverseAnalytics.Domain.Exceptions;
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
        private readonly ISupplyService _supplyService;
        private readonly ILogger<SuppliersController> _logger;

        public SuppliersController(ISupplierService supplierService, IAddressService addressService, 
            IPhoneService phoneService, IDebtService debtService, ISupplyService supplyService,
            ILogger<SuppliersController> logger)
        {
            _supplierService = supplierService;
            _addressService = addressService;
            _phoneService = phoneService;
            _debtService = debtService;
            _supplyService = supplyService;
            _logger = logger;
        }

        #region CRUD

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetSuppliers(string? searchString)
        {
            try
            {
                var suppliers = await _supplierService.GetAllSuppliersAsync(searchString);

                if (suppliers is null || !suppliers.Any())
                {
                    return Ok("There are no suppliers.");
                }

                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errr retrieving suppliers", ex.Message);
                return StatusCode(500, "There was an error retrieving Suppliers. Please, try again later.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDto>> GetSupplierById(int id)
        {
            try
            {
                var supplier = await _supplierService.GetSupplierByIdAsync(id);

                if (supplier is null)
                {
                    return NotFound($"Supplier with id: {id} does not exist.");
                }

                return Ok(supplier);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Supplier with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Supplier with id: {id}. Please, try again later.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SupplierDto>> CreateSupplier(SupplierForCreateDto supplierToCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Supplier to create is not valid.");
                }

                var createdSupplier = await _supplierService.CreateSupplierAsync(supplierToCreate);

                return Ok(createdSupplier);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating new Supplier.", ex.Message);
                return StatusCode(500, "There was an error creating a new Supplier. Please, try again later.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSupplier(SupplierForUpdateDto supplierToUpdate, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Supplier to update is not valid.");
                }

                if (supplierToUpdate.Id != id)
                {
                    return BadRequest($"Route id: {id} does not match with Supplier id: {supplierToUpdate.Id}");
                }

                await _supplierService.UpdateSupplierAsync(supplierToUpdate);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating supplier with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error updating Supplier with id: {id}. Please, try again later.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSupplier(int id)
        {
            try
            {
                await _supplierService.DeleteSupplierAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting Supplier with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error deleting Supplier with id: {id}. Please, try again later.");
            }
        }

        #endregion

        #region Addresses

        [HttpGet("{supplierId}/addresses")]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetSupplierAddresses(int supplierId)
        {
            try
            {
                var addresses = await _addressService.GetAllByPersonIdAsync(supplierId);

                if (addresses is null || !addresses.Any())
                {
                    return Ok($"Supplier with id: {supplierId} does not have any addresses.");
                }

                return Ok(addresses);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Supplier Addresses for Supplier with id: {supplierId}", ex.Message);
                return StatusCode(500, "There was an error retrieving Supplier Addresses. Please, try again later.");
            }
        }

        [HttpGet("{supplierId}/addresses/{addressId}")]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetSupplierAddressById(int supplierId, int addressId)
        {
            try
            {
                var address = await _addressService.GetAddressByPersonAndAddressIdAsync(supplierId, addressId);

                if (address is null)
                {
                    return NotFound($"Supplier with id: {supplierId} does not have an Address with id: {addressId}");
                }

                return Ok(address);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Supplier Address with Supplier id: {supplierId} and address id: {addressId}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Supplier Address with Supplier id: {supplierId} and Address id: {addressId}.");
            }
        }

        [HttpPost("{supplierId}/addresses")]
        public async Task<ActionResult<AddressDto>> CreateSupplierAddressAsync([FromBody] AddressForCreateDto addressToCreate, int supplierId)
        {
            try
            {
                if (addressToCreate is null)
                {
                    return BadRequest("Supplier Address to create cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Supplier Address to create is not valid.");
                }

                if (addressToCreate.PersonId != supplierId)
                {
                    return BadRequest($"Supplier Id: {addressToCreate.PersonId} does not match with route id: {supplierId}");
                }

                var createdAddress = await _addressService.CreateAddressAsync(addressToCreate);

                if (createdAddress is null)
                {
                    return StatusCode(500,
                        $"Something went wrong while adding address number for Supplier with id: {supplierId}. Please, try again later.");
                }

                return Ok(createdAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while adding address number for Supplier with id: {supplierId}.", ex.Message);
                return StatusCode(500, $"There was an error adding address number for Supplier with id: {supplierId}.");
            }
        }

        [HttpPut("{supplierId}/addresses/{addressId}")]
        public async Task<ActionResult> UpdateSupplierAddressAsync([FromBody] AddressForUpdateDto addressToUpdate, int supplierId, int addressId)
        {
            try
            {
                if (addressToUpdate is null)
                {
                    return BadRequest("Supplier Address to update cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Supplier Address to update is not valid.");
                }

                if (addressToUpdate.Id != addressId)
                {
                    return BadRequest($"Supplier id: {addressToUpdate.Id}, does not match with route id: {addressId}.");
                }

                if (addressToUpdate.PersonId != supplierId)
                {
                    return BadRequest($"Supplier id: {addressToUpdate.PersonId} does not match with route id: {supplierId}");
                }

                await _addressService.UpdateAddresAsync(addressToUpdate);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating address for Supplier with id: {supplierId}.", ex.Message);
                return StatusCode(500, $"There was an error updating address number for Supplier with id: {supplierId}. Please, try again later.");
            }
        }

        [HttpDelete("{supplierId}/addresses/{addressId}")]
        public async Task<ActionResult> DeleteSupplierAddresssync(int supplierId, int addressId)
        {
            try
            {
                await _addressService.DeleteAddressAsync(addressId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogError($"Supplier address with Supplier id: {supplierId}, address id: {addressId} was not found while deleting.", ex.Message);
                return NotFound($"Supplier address with id: {addressId} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting address for Supplier with id: {supplierId} and address id: {addressId}.", ex.Message);
                return StatusCode(500, $"There was an error deleting address for Supplier with id: {supplierId} and address id: {addressId}.");
            }
        }

        #endregion

        #region Phones

        [HttpGet("{supplierId}/phones")]
        public async Task<ActionResult<IEnumerable<PhoneDto>>> GetPhonesBysupplierIdAsync(int supplierId)
        {
            try
            {
                var phones = await _phoneService.GetAllByPersonIdAsync(supplierId);

                if (phones is null || !phones.Any())
                {
                    return Ok($"Supplier with id: {supplierId} does not have any phone numbers.");
                }

                return Ok(phones);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Supplier Phones for Supplier with id: {supplierId}", ex.Message);
                return StatusCode(500, "There was an error retrieving Supplier Phones. Please, try again later.");
            }
        }

        [HttpGet("{supplierId}/phones/{phoneId}")]
        public async Task<ActionResult<PhoneDto>> GetPhoneBySupplierAndPhoneIdAsync(int supplierId, int phoneId)
        {
            try
            {
                var phone = await _phoneService.GetByPersonAndPhoneIdAsync(supplierId, phoneId);

                if(phone is null)
                {
                    return Ok($"Supplier with id: {supplierId} does not have phone with id: {phoneId}");
                }

                return Ok(phone);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while retrieving Phones for Supplier with id: {supplierId} and Phone id: {phoneId}", ex.Message);
                return StatusCode(500, "There was an error retrieving Supplier Phones. Please, try again later.");
            }
        }

        [HttpPost("{supplierId}/phones")]
        public async Task<ActionResult<PhoneDto>> CreateSupplierPhoneAsync([FromBody] PhoneForCreateDto phoneToCreate, int supplierId)
        {
            try
            {
                if (phoneToCreate is null)
                {
                    return BadRequest("Supplier Phone to create cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Supplier Phone to create is not valid.");
                }

                if(phoneToCreate.PersonId != supplierId)
                {
                    return BadRequest($"Supplier Id: {phoneToCreate.PersonId} does not match with route id: {supplierId}");
                }

                var createdPhone = await _phoneService.CreatePhoneAsync(phoneToCreate);

                if (createdPhone is null)
                {
                    return StatusCode(500, $"Something went wrong while adding phone number for Supplier with id: {supplierId}. Please, try again later.");
                }

                return Ok(createdPhone);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while adding phone number for Supplier with id: {supplierId}.", ex.Message);
                return StatusCode(500, $"There was an error adding phone number for Supplier with id: {supplierId}.");
            }
        }

        [HttpPut("{supplierId}/phones/{phoneId}")]
        public async Task<ActionResult> UpdateSupplierPhoneAsync([FromBody] PhoneForUpdateDto phoneToUpdate, int supplierId, int phoneId)
        {
            try
            {
                if(phoneToUpdate is null)
                {
                    return BadRequest("Supplier Phone to update cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Supplier Phone to update is not valid.");
                }

                if (phoneToUpdate.Id != phoneId)
                {
                    return BadRequest($"Phone id: {phoneToUpdate.Id}, does not match with route id: {phoneId}.");
                }

                if(phoneToUpdate.PersonId != supplierId)
                {
                    return BadRequest($"Supplier id: {phoneToUpdate.PersonId} does not match with route id: {supplierId}");
                }

                await _phoneService.UpdatePhoneAsync(phoneToUpdate);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while updating phone for Supplier with id: {supplierId}.", ex.Message);
                return StatusCode(500, $"There was an error updating phone number for Supplier with id: {supplierId}. Please, try again later.");
            }
        }

        [HttpDelete("{supplierId}/phones/{phoneId}")]
        public async Task<ActionResult> DeleteSupplierPhoneAsync(int supplierId, int phoneId)
        {
            try
            {
                await _phoneService.DeletePhoneAsync(phoneId);
                return NoContent();
            }
            catch(NotFoundException ex)
            {
                _logger.LogError($"Supplier phone with Supplier id: {supplierId}, phone id: {phoneId} was not found while deleting.", ex.Message);
                return NotFound($"Supplier phone with id: {phoneId} was not found.");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while deleting phone for Supplier with id: {supplierId} and phone id: {phoneId}.", ex.Message);
                return StatusCode(500, $"There was an error deleting phone for Supplier with id: {supplierId} and phone id: {phoneId}.");
            }
        }

        #endregion

        #region Debts

        [HttpGet("{supplierId}/debts")]
        public async Task<ActionResult<IEnumerable<DebtDto>>> GetSupplierDebtsAsync(int supplierId)
        {
            try
            {
                var debts = await _debtService.GetAllDebtsByPersonIdAsync(supplierId);

                if (debts is null || !debts.Any())
                {
                    return Ok("This Supplier does not have any Debts.");
                }

                return Ok(debts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Debts for Supplier with id: {supplierId}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Debts for Supplier with id: {supplierId}. Please, try again later.");
            }
        }

        [HttpGet("{supplierId}/debts/{debtId}")]
        public async Task<ActionResult<DebtDto>> GetDebtBySupplierAndDebtIdAsync(int supplierId, int debtId)
        {
            try
            {
                var debt = await _debtService.GetByPersonAndDebtId(supplierId, debtId);

                if (debt is null)
                {
                    return NotFound($"Supplier with id: {supplierId} does not have Debt with id: {debtId}.");
                }

                return Ok(debt);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Debt for Supplier with id: {supplierId} and Debt id: {debtId}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Debts for Supplier with id: {supplierId} and Debt id: {debtId}. Please, try again later.");
            }
        }

        [HttpPost("{supplierId}/debts")]
        public async Task<ActionResult<DebtDto>> CreateSupplierDebtAsync([FromBody] DebtForCreateDto debtToCreate, int supplierId)
        {
            try
            {
                if (debtToCreate is null)
                {
                    return BadRequest("Supplier Debt cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Supplier Debt to create is not valid.");
                }

                if (debtToCreate.PersonId != supplierId)
                {
                    return BadRequest($"Supplier Id: {debtToCreate.PersonId} does not match with route id: {supplierId}");
                }

                var createdSupplierDebt = await _debtService.CreateDebtAsync(debtToCreate);

                if (createdSupplierDebt is null)
                {
                    return StatusCode(500,
                        "Something went wrong while creating new Supplier Debt. Please, try again later.");
                }

                return Ok(createdSupplierDebt);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while creating new Supplier Debt.", ex.Message);
                return StatusCode(500, "There was an error creating new Supplier Debt. Please, try again later");
            }
        }

        [HttpPut("{supplierId}/debts/{debtId}")]
        public async Task<ActionResult> UpdateSupplierDebtAsync([FromBody] DebtForUpdateDto debtToUpdate, int supplierId, int debtId)
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

                if (debtId != debtToUpdate.Id)
                {
                    return BadRequest($"Debt id: {debtToUpdate.Id} does not match with route id: {debtId}.");
                }

                if (supplierId != debtToUpdate.PersonId)
                {
                    return BadRequest($"Supplier id: {debtToUpdate.PersonId} does not match with route id: {supplierId}.");
                }

                await _debtService.UpdateDebtAsync(debtToUpdate);

                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogError($"Supplier debt with id: {debtToUpdate?.Id} was not found while updating.", ex.Message);
                return NotFound($"Supplier Debt with id: {debtToUpdate?.Id} does not exist.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating Supplier Debt with id: {debtToUpdate?.Id}", ex.Message);
                return StatusCode(500,
                    $"There was an error updating Supplier Debt with id: {debtToUpdate?.Id}. Please, try again later");
            }
        }

        [HttpDelete("{supplierId}/debts/{debtId}")]
        public async Task<ActionResult> DeleteSupplierDebtAsync(int debtId)
        {
            try
            {
                await _debtService.DeleteDebtAsync(debtId);

                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogError($"Supplier Debt with id: {debtId} was not found while deleting.", ex.Message);
                return NotFound($"Supplier Debt with id: {debtId} does not exist.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting Supplier Debt with id {debtId}.", ex.Message);
                return StatusCode(500, $"There was an error deleting Supplier Debt with id: {debtId}. Please, try again later.");
            }
        }

        #endregion

        #region Supplies

        [HttpGet("{supplierId}/supplies")]
        public async Task<ActionResult<IEnumerable<SupplyDto>>> GetAllSuppliesBySupplierIdAsync(int supplierId)
        {
            try
            {
                var supplies = await _supplyService.GetAllSuppliesBySupplierIdAsync(supplierId);

                if (supplies is null || !supplies.Any())
                {
                    return Ok($"Supplier with id: {supplierId} has no Supplies.");
                }

                return Ok(supplies);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving Supplies for Supplier with id: {supplierId}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Supplies for Supplier with id: {supplierId}.");
            }
        }

        [HttpGet("{supplierId}/supplies/{supplyId}")]
        public async Task<ActionResult<SupplyDto>> GetSupplyByIdAsync(int supplierId, int supplyId)
        {
            try
            {
                var supply = await _supplyService.GetBySupplierAndSupplyIdAsync(supplierId, supplyId);

                if (supply is null)
                {
                    return NotFound($"Supplier with id: {supplierId} has no Supply with id: {supplyId}.");
                }

                return Ok(supply);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving Supply with id: {supplyId} and Supplier id: {supplierId}.", ex.Message);
                return StatusCode(500, 
                    $"There was an error retrieving Supply for Supplier with id: {supplierId} and Supply id: {supplyId}.");
            }
        }

        [HttpPost("{supplierId}/supplies")]
        public async Task<ActionResult<SupplyDto>> CreateSupplyAsync([FromBody] SupplyForCreate supplyToCreate, int supplierId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Supply to create is not valid.");
                }

                if(supplyToCreate.SupplierId != supplierId)
                {
                    return BadRequest($"Supplier id: {supplyToCreate.SupplierId} does not match with route id: {supplierId}.");
                }

                var createdSupply = await _supplyService.CreateSupplyAsync(supplyToCreate);

                return Ok(createdSupply);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating Supply.", ex.Message);
                return StatusCode(500, "There was an error creating a new Supply.");
            }
        }

        [HttpPut("{supplierId}/supplies/{supplyId}")]
        public async Task<ActionResult> UpdateSupplyAsync([FromBody] SupplyForUpdate supplyToUpdate, int supplierId, int supplyId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Supply to update is not valid.");
                }

                if (supplyToUpdate.Id != supplyId)
                {
                    return BadRequest($"Supply id: {supplyToUpdate.Id} does not match with route id: {supplyId}.");
                }

                if (supplyToUpdate.SupplierId != supplierId)
                {
                    return BadRequest($"Supplier id: {supplyToUpdate.SupplierId} does not match with route id: {supplierId}.");
                }

                await _supplyService.UpdateSupplyAsync(supplyToUpdate);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating Supplly with id: {supplyId}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Supply with id: {supplyId}.");
            }
        }

        [HttpDelete("{supplierId}/supplies/{supplyId}")]
        public async Task<ActionResult> DeleteSupplyAsync(int supplyId)
        {
            try
            {
                await _supplyService.DeleteSupplyAsync(supplyId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting Supplly with id: {supplyId}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Supply with id: {supplyId}.");
            }
        }

        #endregion
    }
}
