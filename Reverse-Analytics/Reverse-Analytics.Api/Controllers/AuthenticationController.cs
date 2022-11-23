using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.Authentication;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationService _authService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAsync(AuthenticationRequest userRequest)
        {
            try
            {
                var result = await _authService.LoginAsync(userRequest);

                if (!result.IsSuccess)
                {
                    return Unauthorized(result);
                }

                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while logging in user: {userRequest.UserName}", ex.Message);
                return StatusCode(500, "There was an error while logging in user. Please, try again later.");
            }
        }
    }
}
