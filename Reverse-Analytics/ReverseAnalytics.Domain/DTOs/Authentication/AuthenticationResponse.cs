namespace ReverseAnalytics.Domain.DTOs.Authentication
{
    public record AuthenticationResponse(string Message, bool IsSuccess, IEnumerable<string> Errors, DateTime? ExpireDate)
    {
        public AuthenticationResponse(string message, bool isSuccess, DateTime? expireDate) :
            this(message, isSuccess, new List<string>(), expireDate)
        {
        }
    }
}