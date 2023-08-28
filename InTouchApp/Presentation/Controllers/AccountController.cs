using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InTouchApi.Presentation.Controllers
{
    [ApiController]
    [Route("account")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<AccountDto>> GetAccountAsync()
        {
            var accountDto = await _service.GetAccountAsync();
            return StatusCode(StatusCodes.Status200OK, accountDto);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAccountAsync([FromBody] UpdateAccountDto updateAccountDto)
        {
            await _service.UpdateAccountAsync(updateAccountDto);
            return StatusCode(StatusCodes.Status204NoContent, updateAccountDto);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAccountAsync()
        {
            await _service.DeleteAccountAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
