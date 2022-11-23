namespace ReverseAnalytics.Domain.DTOs.Authentication
{
    public class AuthenticationResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? ExpireDate { get; set; }

        public AuthenticationResponse(string message, bool isSuccess, DateTime? expireDate)
        {
            Message = message;
            IsSuccess = isSuccess;
            ExpireDate = expireDate;
            Errors = new List<string>();
        }

        public AuthenticationResponse(string message, bool isSuccess, IEnumerable<string> errors, DateTime? expireDate)
        {
            Message = message;
            IsSuccess = isSuccess;
            Errors = errors;
            ExpireDate = expireDate;
        }
    }
}
