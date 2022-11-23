namespace ReverseAnalytics.Domain.DTOs.PasswordReset
{
    public class PasswordResetResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ResponseError> Errors { get; set; }
    }

    public class ResponseError
    {
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
