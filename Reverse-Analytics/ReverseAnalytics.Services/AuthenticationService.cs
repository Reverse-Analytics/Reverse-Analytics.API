using Microsoft.AspNetCore.Identity;
using ReverseAnalytics.Domain.DTOs.Authentication;
using ReverseAnalytics.Domain.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;

namespace ReverseAnalytics.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthenticationService(UserManager<IdentityUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<AuthenticationResponse> LoginAsync(AuthenticationRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            var authenticated = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!authenticated)
            {
                List<string> errors = new()
                {
                    "Invalid user name or password."
                };

                return new AuthenticationResponse("", false, errors, null);
            }

            var token = _tokenService.CreateToken(request);
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthenticationResponse(tokenString, true, token.ValidTo);
        }
    }
}
