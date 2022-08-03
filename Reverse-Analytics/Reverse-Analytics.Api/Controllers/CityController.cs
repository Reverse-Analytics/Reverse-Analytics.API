using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.City;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;
        private readonly ICityService _service;

        public CityController(ILogger<CityController> logger, ICityService service)
        {
            _logger = logger;
            _service = service;
        }

        #region Actions

        [HttpGet]
        public async Task<ActionResult<CityDto>> GetCitiesAsync(string? searchString)
        {
            try
            {
                var cities = await _service.GetAllCitiesAsync(searchString);

                if(cities is null)
                {
                    return Ok("No cities were found.");
                }

                return Ok(cities);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "There was an error retrieving customers. Please, try again later.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> GetCityByIdAsync(int id)
        {
            try
            {
                var city = await _service.GetCityByIdAsync(id);

                if(city is null)
                {
                    return NotFound($"City with id: {id} does not exist.");
                }

                return Ok(city);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error retrieving city with id: {id}", ex.Message);
                return StatusCode(500, $"There was an error retrieving city with id: {id}.");
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<CityDto>> Post([FromBody] CityForCreateDto cityToCreate)
        {
            try
            {
                if(cityToCreate is null)
                {
                    return BadRequest("City cannot be null or empty.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("The city is not valid for creation.");
                }

                var cityDto = await _service.CreateCityAsync(cityToCreate);

                if(cityDto is null)
                {
                    return StatusCode(500, "Seomthing went wrong while creating a new city.");
                }

                return Ok(cityDto);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error creating a new city.", ex.Message);
                return StatusCode(500, "There was an error creating new city. Please, try again later.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CityForUpdateDto cityToUpdate)
        {
            try
            {
                if(cityToUpdate is null)
                {
                    return BadRequest("City to update cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("City is not valid for update");
                }

                if(cityToUpdate.Id != id)
                {
                    return BadRequest($"City id: {cityToUpdate.Id} does not match with route id: {id}.");
                }

                await _service.UpdateCityAsync(cityToUpdate);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error updating city with id: {id}", ex);
                return StatusCode(500, $"There was an error updating city with id: {id}. Please, try again later.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteCityAsync(id);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error deleting city with id: {id}.", ex);
                return StatusCode(500, $"There was an error deleting city with id: {id}. Please, try again later.");
            }
        }

        #endregion
    }
}
