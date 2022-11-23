using ReverseAnalytics.Domain.DTOs.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        public JwtSecurityToken CreateToken(AuthenticationRequest request);
    }
}
