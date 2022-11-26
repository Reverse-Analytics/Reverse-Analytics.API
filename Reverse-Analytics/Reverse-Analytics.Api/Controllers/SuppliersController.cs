using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.Supplier;
using ReverseAnalytics.Domain.DTOs.SupplierDebt;
using ReverseAnalytics.Domain.DTOs.SupplierPhone;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/suppliers")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _service;
        private readonly ISupplierPhoneService _supplierPhoneService;
        private readonly ISupplierDebtService _supplierDebtService;
        private readonly ILogger<SuppliersController> _logger;

        public SuppliersController(ISupplierService service, ISupplierPhoneService supplierPhoneService, 
            ISupplierDebtService supplierDebtService, ILogger<SuppliersController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _supplierPhoneService = supplierPhoneService ?? throw new ArgumentException(nameof(supplierPhoneService));
            _supplierDebtService = supplierDebtService ?? throw new ArgumentException(nameof(supplierDebtService));
        }

        #region CRUD

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetSuppliers(string? searchString)
        {
            try
            {
                var suppliers = await _service.GetAllSuppliersAsync(searchString);

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
                var supplier = await _service.GetSupplierByIdAsync(id);

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

                var createdSupplier = await _service.CreateSupplierAsync(supplierToCreate);

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

                await _service.UpdateSupplierAsync(supplierToUpdate);

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
                await _service.DeleteSupplierAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting Supplier with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error deleting Supplier with id: {id}. Please, try again later.");
            }
        }

        #endregion

        #region Supplier Phones

        [HttpGet("{supplierId}/phones")]
        public async Task<ActionResult<IEnumerable<SupplierPhoneDto>>> GetAllSupplierPhones(int supplierId)
        {
            try
            {
                var supplierPhones = await _supplierPhoneService.GetSupplierPhonesBySupplierIdAsync(supplierId);

                if (supplierPhones is null || !supplierPhones.Any())
                {
                    return Ok($"Supplier with id: {supplierId} does not have any phone numbers.");
                }

                return Ok(supplierPhones);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error retreiving Phones for Supplier with id: {supplierId}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Phones for Supplier with id: {supplierId}. Please, try again later.");
            }
        }

        [HttpGet("{supplierId}/phones/{phoneId}")]
        public async Task<ActionResult<SupplierPhoneDto>> GetSupplierPhoneById(int supplierId, int phoneId)
        {
            try
            {
                var supplierPhone = await _supplierPhoneService.GetSupplierPhoneBySupplierAndPhoneIdAsync(supplierId, phoneId);

                if(supplierPhone is null)
                {
                    return NotFound($"Supplier Phone with Supplier id: {supplierId} and Phone id: {phoneId} does not exist.");
                }

                return Ok(supplierPhone);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error retrieving Supplier Phone with Supplier id: {supplierId} & Phone id: {phoneId}.", ex.Message);
                return StatusCode(500, $"There was an error retreiving Supplier Phone with Supplier id: {supplierId} & Phone id{phoneId}. Please, try again later.");
            }
        }

        [HttpPost("{supplierId}/phones")]
        public async Task<ActionResult<SupplierPhoneDto>> CreateSupplierPhone(int supplierId, SupplierPhoneForCreate supplierPhoneToCreate)
        {
            try
            {
                if (supplierId != supplierPhoneToCreate.SupplierId)
                {
                    return BadRequest($"Supplier id: {supplierPhoneToCreate.SupplierId} does not match with route id: {supplierId}.");
                }

                var createdSupplierPhone = await _supplierPhoneService.CreateSupplierPhoneAsync(supplierPhoneToCreate);

                return Ok(createdSupplierPhone);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while updating Supplier Phone for Supplier with id: {supplierId}.", ex.Message);
                return StatusCode(500, $"There was an error creating a new Phone for Supplier with id: {supplierId}. Please, try again later.");
            }
        }

        [HttpPut("{supplierId}/phones/{phoneId}")]
        public async Task<ActionResult> UpdateSupplierPhoneAsync(int supplierId, int phoneId, SupplierPhoneForUpdate supplierToUpdate)
        {
            try
            {
                if(supplierId != supplierToUpdate.SupplierId)
                {
                    return BadRequest($"Route id: {supplierId} does not match with Supplier Id: {supplierToUpdate.SupplierId}.");
                }

                if(phoneId != supplierToUpdate.Id)
                {
                    return BadRequest($"Route id: {phoneId} does not match with Phone Id: {supplierToUpdate.Id}.");
                }

                await _supplierPhoneService.UpdateSupplierPhoneAsync(supplierToUpdate);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while updating Supplier Phone for supplier with id: {supplierId} and Phone id: {phoneId}.", ex.Message);
                return StatusCode(500, $"There was an error updating Phone for Supplier with id: {supplierId} and Phone id: {phoneId}. Please, try again later.");
            }
        }

        [HttpDelete("{supplierId}/phones/{phoneId}")]
        public async Task<ActionResult> DeleteSupplierPhoneAsync(int supplierId, int phoneId)
        {
            try
            {
                await _supplierPhoneService.DeleteSupplierPhoneAsync(phoneId);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while deleting Supplier Phone for Supplier with id: {supplierId} and Phone id: {phoneId}.", ex.Message);
                return StatusCode(500, $"There was an error deleting Phone for Supplier with id: {supplierId} and Phone id: {phoneId}. Please, try again later.");
            }
        }

        #endregion

        #region Supplier Debts

        [HttpGet("{supplierId}/debts")]
        public async Task<ActionResult<IEnumerable<SupplierDebtDto>>> GetAllSupplierDebtsBySupplierIdAsync(int supplierId)
        {
            try
            {
                var supplierDebts = await _supplierDebtService.GetAllSupplierDebtsBySupplierIdAsync(supplierId);

                if(supplierDebts is null || !supplierDebts.Any())
                {
                    return Ok($"Supplier with id: {supplierId} has no Debts.");
                }

                return Ok(supplierDebts);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while retrieving Debts for supplier with id: {supplierId}.", ex.Message);
                return StatusCode(500, $"There was an error while retrieving Debt for Supplier with Id: {supplierId}.");
            }
        }
        
        [HttpGet("{supplierId}/debts/{debtId}")]
        public async Task<ActionResult<SupplierDebtDto>> GetSupplierDebtBySupplierAndDebtIdAsync(int supplierId, int debtId)
        {
            try
            {
                var supplierDebt = await _supplierDebtService.GetSupplierDebtBySupplierAndDebtIdAsync(supplierId, debtId);

                if(supplierDebt is null)
                {
                    return Ok($"There is no Debt with id: {debtId} and Supplier id: {supplierId}.");
                }

                return Ok(supplierDebt);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while retrieving Debts for supplier with id: {supplierId}.", ex.Message);
                return StatusCode(500, $"There was an error while retrieving Debt for Supplier with id: {supplierId} and Debt id: {debtId}.");
            }
        }

        [HttpPost("{supplierId}/debts")]
        public async Task<ActionResult<SupplierDebtDto>> CreateSupplierDebtAsync([FromBody] SupplierDebtForCreateDto supplierDebtToCreate, int supplierId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest($"Supplier Debt to create is not valid.");
                }

                var createdSupplierDebt = await _supplierDebtService.CreateSupplierDebtAsync(supplierDebtToCreate);

                return Ok(createdSupplierDebt);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while creating Debt for Supplier with id: {supplierId}.", ex.Message);
                return StatusCode(500, $"There was an error while creating Debt for Supplier with Id: {supplierId}.");
            }
        }

        [HttpPut("{supplierId}/debts/{debtId}")]
        public async Task<ActionResult> UpdateSupplierDebt([FromBody]SupplierDebtForUpdateDto supplierDebtToUpdate, int supplierId, int debtId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Supplier Debt is not valid to update.");
                }

                if(supplierDebtToUpdate.Id != debtId)
                {
                    return BadRequest($"Supplier Debt id: {supplierDebtToUpdate.Id} does not match with route id: {debtId}.");
                }

                await _supplierDebtService.UpdateSupplierDebtAsync(supplierDebtToUpdate);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while updating Debt for Supplier with id: {supplierId} and Debt id: {debtId}.", ex.Message);
                return StatusCode(500, $"There was an error while updating Debt for Supplier with Id: {supplierId} and Debt id: {debtId}.");
            }
        }

        [HttpDelete("{supplierId}/debts/{debtId}")]
        public async Task<ActionResult> DeleteSupplierDebt(int supplierId, int debtId)
        {
            try
            {
                await _supplierDebtService.DeleteSupplierDebtAsync(debtId);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while deleting Debt for Supplier with id: {supplierId} and Debt id: {debtId}.", ex.Message);
                return StatusCode(500, $"There was an error while deleting Debt for Supplier with Id: {supplierId} and Debt id: {debtId}.");
            }
        }

        #endregion
    }
}
