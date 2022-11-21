using ReverseAnalytics.Domain.DTOs.UserAccount;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface IAccountService
    {
        public Task<IEnumerable<UserAccountDto>> GetAllAccountsAsync();
        public Task<UserAccountDto> GetAccountByIdAsync(string id);
        public Task<UserAccountDto> GetAccountByLoginAsync(string name);
        public Task CreateAccountAsync(UserAccountForCreateDto userAccountToCreate);
        public Task UpdateAccountAsync(UserAccountForUpdateDto userAccountToUpdate);
        public Task DeleteAccountAsync(string id);
    }
}
