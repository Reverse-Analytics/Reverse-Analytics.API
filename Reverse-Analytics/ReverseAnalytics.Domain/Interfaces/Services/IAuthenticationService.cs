using ReverseAnalytics.Domain.DTOs.User;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface IAuthenticationService
    {
        public Task<string?> Authenticate(UserDto user);
    }
}
