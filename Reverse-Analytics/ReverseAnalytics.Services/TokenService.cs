using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReverseAnalytics.Domain.DTOs.Authentication;
using ReverseAnalytics.Domain.Interfaces.Services;
using ReverseAnalytics.Infrastructure.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReverseAnalytics.Services
{
    public class TokenService : ITokenService
    {
        private readonly CustomTokenOptions _tokenOptions;

        public TokenService(IOptions<CustomTokenOptions> tokenOptions)
        {
            _tokenOptions = tokenOptions.Value;
        }

        public JwtSecurityToken CreateToken(AuthenticationRequest request)
        {
            DateTime now = DateTime.Now;

            var accessTokenExpiration = now.AddDays(_tokenOptions.AccessTokenExpiration);
            var securityKey = GetSymmetricSecurityKey(_tokenOptions.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken token = new(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                expires: accessTokenExpiration,
                notBefore: now,
                claims: GetClaims(request, new List<string>()),
                signingCredentials: signingCredentials);

            return token;
        }

        private IEnumerable<Claim> GetClaims(AuthenticationRequest request, List<string> audiences)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            claims.AddRange(audiences.Select(p => new Claim(JwtRegisteredClaimNames.Aud, p)));
            return claims;
        }

        public static SecurityKey GetSymmetricSecurityKey(string securityKey)
            => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
    }
}
