using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.User;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthenticationService authService, ILogger<AuthenticationController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> Authenticate(UserDto requestUser)
        {
            var authToken = await _authService.Authenticate(requestUser);

            if (string.IsNullOrEmpty(authToken))
            {
                return Unauthorized();
            }

            return Ok(authToken);
        }
    }
}
