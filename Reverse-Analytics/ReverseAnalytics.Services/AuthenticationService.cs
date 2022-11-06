using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ReverseAnalytics.Domain.DTOs.User;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    /// !!! TODO
    /// This implementation is basic just for the sake
    /// of testing UI. The logic should be changed,
    /// along with security concerns provided.
    ///
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthenticationService(ICommonRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<string?> Authenticate(UserDto requestUser)
        {
            var user = await ValidateCredentials(requestUser.UserName, requestUser.Password);

            if(user is null)
            {
                return null;
            }

            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("user_name", user.UserName)
            };

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
               .WriteToken(jwtSecurityToken);

            return tokenToReturn;
        }

        private async Task<UserDto?> ValidateCredentials(string userName, string password)
        {
            if (userName is null || password is null)
            {
                return null;
            }

            var user = await _repository.User.FindByNameAndPassword(userName, password);

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

    }
}
