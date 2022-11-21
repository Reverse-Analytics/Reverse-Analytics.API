using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.DTOs.UserAccount;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public AccountService(UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task CreateAccountAsync(UserAccountForCreateDto userAccountToCreate)
        {
            try
            {
                var user = _mapper.Map<IdentityUser>(userAccountToCreate);

                var result = await _userManager.CreateAsync(user, userAccountToCreate.Password);

                await _userManager.AddToRoleAsync(user, "Visitor");
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task DeleteAccountAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if(user != null)
                {
                    await _userManager.DeleteAsync(user);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserAccountDto> GetAccountByIdAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                var userDto = _mapper.Map<UserAccountDto>(user);

                return userDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserAccountDto> GetAccountByLoginAsync(string name)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(name);

                var userDto = _mapper.Map<UserAccountDto>(user);

                return userDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserAccountDto>> GetAllAccountsAsync()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();

                var userDtos = _mapper.Map<IEnumerable<UserAccountDto>>(users);

                return userDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAccountAsync(UserAccountForUpdateDto userAccountToUpdate)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userAccountToUpdate.Id);

                user.UserName = userAccountToUpdate.UserName;
                user.NormalizedUserName = userAccountToUpdate.UserName.ToUpper();

                var result = await _userManager.UpdateAsync(user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
