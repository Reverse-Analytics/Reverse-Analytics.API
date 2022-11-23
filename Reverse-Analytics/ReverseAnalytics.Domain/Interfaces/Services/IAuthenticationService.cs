using ReverseAnalytics.Domain.DTOs.Authentication;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> LoginAsync(AuthenticationRequest request);

    }
}
