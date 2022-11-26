using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.Address;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Route("api/addresses")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly ILogger<AddressesController> _logger;

        public AddressesController(IAddressService addressService, ILogger<AddressesController> logger)
        {
            _addressService = addressService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAllAddressesAsync()
        {
            try
            {
                var addresses = await _addressService.GetAllAddressesAsync();

                if (addresses is null || !addresses.Any())
                {
                    return Ok($"There are no Addresses.");
                }

                return Ok(addresses);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Addresses.", ex.Message);
                return StatusCode(500, "There was an error retrieving Addresses. Please, try again later.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAddressByIdAsync(int id)
        {
            try
            {
                var address = await _addressService.GetAddressByIdAsync(id);

                if (address is null)
                {
                    return NotFound($"Address with id: {id} does not exist.");
                }

                return Ok(address);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Address with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Address with id: {id}.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AddressDto>> CreateAddressAsync([FromBody] AddressForCreateDto addressToCreate)
        {
            try
            {
                if (addressToCreate is null)
                {
                    return BadRequest("Address to create cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Address to create is not valid.");
                }

                var createdAddress = await _addressService.CreateAddressAsync(addressToCreate);

                if (createdAddress is null)
                {
                    return StatusCode(500, $"Something went wrong while adding address. Please, try again later.");
                }

                return Ok(createdAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while adding new Address.", ex.Message);
                return StatusCode(500, $"There was an error adding Address.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAddressAsync([FromBody] AddressForUpdateDto addressToUpdate, int id)
        {
            try
            {
                if (addressToUpdate is null)
                {
                    return BadRequest("Address to update cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Address to update is not valid.");
                }

                if (addressToUpdate.Id != id)
                {
                    return BadRequest($"Address id: {addressToUpdate.Id}, does not match with route id: {id}.");
                }

                await _addressService.UpdateAddresAsync(addressToUpdate);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating Address for with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error updating Address with id: {id}. Please, try again later.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAddresssync(int id)
        {
            try
            {
                await _addressService.DeleteAddressAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogError($"Address with id: {id} was not found while deleting.", ex.Message);
                return NotFound($"Address with id: {id} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting Address with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error deleting Address with id: {id}.");
            }
        }
    }
}
