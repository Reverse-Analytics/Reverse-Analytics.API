namespace ReverseAnalytics.Domain.DTOs.PasswordReset
{
    public record PasswordResetResponse(bool IsSuccess, string Message, List<ResponseError> Errors);

    public class ResponseError
    {
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
