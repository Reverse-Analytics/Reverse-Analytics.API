using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reverse_Analytics.Api.Filters;
using ReverseAnalytics.Domain.DTOs.PasswordReset;
using ReverseAnalytics.Domain.DTOs.UserAccount;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAccountDto>>> GetUserAccountsAsync()
        {
            var userAccounts = await _accountService.GetAllAccountsAsync();

            return Ok(userAccounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserAccountDto>> GetUserAccountByIdAsync(string id)
        {
            var userAccount = await _accountService.GetAccountByIdAsync(id);

            if (userAccount is null) 
                return NotFound($"User account with id: {id} does not exist.");

            return Ok(userAccount);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> CreateUserAccountAsync(UserAccountForCreateDto userAccountToCreate)
        {
            await _accountService.CreateAccountAsync(userAccountToCreate);

            return Created("User Accout was successfully created.", userAccountToCreate);
        }

        [HttpPost("PasswordReset")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<PasswordResetResponse>> ResetPasswordAsync(PasswordResetRequest request)
        {
            var result = await _accountService.ResetPasswordAsync(request);

            if (!result.IsSuccess)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }

            return StatusCode(201, result);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateUserAccountAsync(UserAccountForUpdateDto userAccountToUpdate, string id)
        {
            if (userAccountToUpdate.Id != id)
            {
                return BadRequest($"Account id: {userAccountToUpdate.Id} does not match with route id: {id}.");
            }

            await _accountService.UpdateAccountAsync(userAccountToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAccountByIdAsync(string id)
        {
            await _accountService.DeleteAccountAsync(id);

            return NoContent();
        }
    }
}
