namespace ReverseAnalytics.Domain.DTOs.PasswordReset
{
    public class PasswordResetRequest
    {
        public string UserName { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
}
