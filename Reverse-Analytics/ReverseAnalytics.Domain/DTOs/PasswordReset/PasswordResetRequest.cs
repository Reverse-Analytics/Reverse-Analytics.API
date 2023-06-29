namespace ReverseAnalytics.Domain.DTOs.PasswordReset
{
    public record PasswordResetRequest(string UserName, string NewPassword, string ConfirmPassword, string Token);
}
