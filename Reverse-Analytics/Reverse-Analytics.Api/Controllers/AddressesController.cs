using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reverse_Analytics.Api.Filters;
using ReverseAnalytics.Domain.DTOs.Address;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/addresses")]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAllAddressesAsync()
        {
            var addresses = await _addressService.GetAllAddressesAsync();

            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAddressByIdAsync(int id)
        {
            var address = await _addressService.GetAddressByIdAsync(id);

            if (address is null)
                return NotFound($"Address with id: {id} does not exist.");

            return Ok(address);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<AddressDto>> CreateAddressAsync([FromBody] AddressForCreateDto addressToCreate)
        {
            var createdAddress = await _addressService.CreateAddressAsync(addressToCreate);

            if (createdAddress is null)
                return StatusCode(500,
                    "Something went wrong while creating new Address. Please, try again later.");

            return Created("Address was successfully created.", createdAddress);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateAddressAsync([FromBody] AddressForUpdateDto addressToUpdate, int id)
        {
            if (addressToUpdate.Id != id)
            {
                return BadRequest($"Address id: {addressToUpdate.Id}, does not match with route id: {id}.");
            }

            await _addressService.UpdateAddresAsync(addressToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAddresssync(int id)
        {
            await _addressService.DeleteAddressAsync(id);
            return NoContent();
        }
    }
}