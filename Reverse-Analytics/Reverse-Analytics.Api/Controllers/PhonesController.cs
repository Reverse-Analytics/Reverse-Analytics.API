using Microsoft.AspNetCore.Mvc;
using Reverse_Analytics.Api.Filters;
using ReverseAnalytics.Domain.DTOs.Phone;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        private readonly IPhoneService _phoneService;

        public PhonesController(IPhoneService phoneService)
        {
            _phoneService = phoneService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneDto>>> GetAllPhonesByIdAsync()
        {
            var phones = await _phoneService.GetAllPhonesAsync();

            return Ok(phones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhoneDto>> GetPhoneByIdAsync(int id)
        {
            var phone = await _phoneService.GetByIdAsync(id);

            if (phone is null)
                NotFound($"Phone number with id: {id} does not exist.");

            return Ok(phone);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<PhoneDto>> CreatePhoneAsync([FromBody] PhoneForCreateDto phoneToCreate)
        {
            var createdPhone = await _phoneService.CreatePhoneAsync(phoneToCreate);

            if (createdPhone is null)
                return StatusCode(500,
                        $"Something went wrong while adding phone number. Please, try again later.");

            return Ok(createdPhone);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdatePhoneAsync([FromBody] PhoneForUpdateDto phoneToUpdate, int id)
        {
            if (phoneToUpdate.Id != id)
                return BadRequest($"Phone id: {phoneToUpdate.Id}, does not match with route id: {id}.");

            await _phoneService.UpdatePhoneAsync(phoneToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePhoneAsync(int id)
        {
            await _phoneService.DeletePhoneAsync(id);

            return NoContent();
        }
    }
}
