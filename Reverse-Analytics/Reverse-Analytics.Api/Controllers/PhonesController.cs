using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.Phone;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        private readonly ILogger<PhonesController> _logger;
        private readonly IPhoneService _phoneService;

        public PhonesController(ILogger<PhonesController> logger, IPhoneService phoneService)
        {
            _logger = logger;
            _phoneService = phoneService;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<PhoneDto>>> GetAllPhonesByIdAsync()
        {
            try
            {
                var phones = await _phoneService.GetAllPhonesAsync();

                if (phones is null || !phones.Any())
                {
                    return Ok($"There are no phone numbers.");
                }

                return Ok(phones);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Phone numbers.", ex.Message);
                return StatusCode(500, "There was an error retrieving Phones. Please, try again later.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhoneDto>> GetPhoneByIdAsync(int id)
        {
            try
            {
                var phone = await _phoneService.GetByIdAsync(id);

                if (phone is null)
                {
                    return Ok($"Phone number with id: {id} does not exist.");
                }

                return Ok(phone);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Phone id with: {id}", ex.Message);
                return StatusCode(500, "There was an error retrieving Phones. Please, try again later.");
            }
        }

        [HttpPost()]
        public async Task<ActionResult<PhoneDto>> CreatePhoneAsync([FromBody] PhoneForCreateDto phoneToCreate)
        {
            try
            {
                if (phoneToCreate is null)
                {
                    return BadRequest(" Phone to create cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(" Phone to create is not valid.");
                }

                var createdPhone = await _phoneService.CreatePhoneAsync(phoneToCreate);

                if (createdPhone is null)
                {
                    return StatusCode(500, $"Something went wrong while adding phone number. Please, try again later.");
                }

                return Ok(createdPhone);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while adding Phone number.", ex.Message);
                return StatusCode(500, $"There was an error adding phone number for.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePhoneAsync([FromBody] PhoneForUpdateDto phoneToUpdate, int id)
        {
            try
            {
                if (phoneToUpdate is null)
                {
                    return BadRequest(" Phone to update cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(" Phone to update is not valid.");
                }

                if (phoneToUpdate.Id != id)
                {
                    return BadRequest($"Phone id: {phoneToUpdate.Id}, does not match with route id: {id}.");
                }

                await _phoneService.UpdatePhoneAsync(phoneToUpdate);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating phone with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error updating phone number with id: {id}. Please, try again later.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePhoneAsync(int id)
        {
            try
            {
                await _phoneService.DeletePhoneAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogError($"Phone with id: {id} was not found.", ex.Message);
                return NotFound($"Phone with id: {id} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting phone number with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error while deleting phone number with id: {id}.");
            }
        }
    }
}
