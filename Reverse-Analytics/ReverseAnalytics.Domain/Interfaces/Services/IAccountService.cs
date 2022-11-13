using ReverseAnalytics.Domain.DTOs.UserAccount;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface IAccountService
    {
        public Task<ICollection<UserAccountDto>> GetAllUserAccounts();
        public Task<UserAccountDto> GetuserAccountById(string id);
        public Task<UserAccountDto> GetUserAccountByLogin(string name);
        public Task<UserAccountDto> CreateUserAccount(UserAccountForCreateDto userAccountToCreate);
        public Task UpdateUserAccount(UserAccountForUpdateDto userAccountToUpdate);
        public Task DeleteUserAccount(string id);
    }
}
