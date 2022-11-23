using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(IAccountService accountService, ILogger<AccountsController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAccountDto>>> GetUserAccountsAsync()
        {
            try
            {
                var userAccounts = await _accountService.GetAllAccountsAsync();

                return Ok(userAccounts);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error while retreiving user accounts.", ex.Message);
                return StatusCode(500, "There was an error retreiving user accounts. Please, try again later.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserAccountDto>> GetUserAccountByIdAsync(string id)
        {
            try
            {
                var userAccount = await _accountService.GetAccountByIdAsync(id);

                return Ok(userAccount);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while retreiving user with id : {id}.", ex.Message);
                return StatusCode(500, $"There was an error retreiving user with id: {id}. Please, try again later.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserAccountAsync(UserAccountForCreateDto userAccountToCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await _accountService.CreateAccountAsync(userAccountToCreate);

                return Created("User Accout was successfully created.", userAccountToCreate);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while creating a new user.", ex.Message);
                return StatusCode(500, $"There was an error creating a new user account. Please, try again later.");
            }
        }

        [HttpPost("PasswordReset")]
        public async Task<ActionResult<PasswordResetResponse>> ResetPasswordAsync(PasswordResetRequest request)
        {
            try
            {
                var result = await _accountService.ResetPasswordAsync(request);

                if (!result.IsSuccess)
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);   
                    }
                }

                return StatusCode(201, result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while resetting password for user: {request.UserName}.", ex.Message);
                return StatusCode(500, "There was an error resetting password.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserAccountAsync(UserAccountForUpdateDto userAccountToUpdate, string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if(userAccountToUpdate.Id != id)
                {
                    return BadRequest($"Account id: {userAccountToUpdate.Id} does not match with route id: {id}.");
                }

                await _accountService.UpdateAccountAsync(userAccountToUpdate);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating user with id : {id}.", ex.Message);
                return StatusCode(500, $"There was an error updating user with id: {id}. Please, try again later.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAccountByIdAsync(string id)
        {
            try
            {
                await _accountService.DeleteAccountAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating user with id : {id}.", ex.Message);
                return StatusCode(500, $"There was an error updating user with id: {id}. Please, try again later.");
            }
        }
    }
}
