namespace ReverseAnalytics.Domain.DTOs.UserAccount
{
    public record UserAccountForCreateDto(string UserName, string Password, string ConfirmPassword);
}
