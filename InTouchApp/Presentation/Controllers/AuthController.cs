using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InTouchApi.Presentation.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenDto>> LoginAsync(LoginDto loginDto)
        {
            var tokenDto = await _service.LoginAsync(loginDto);
            return StatusCode(StatusCodes.Status202Accepted, tokenDto);
        }

        [HttpPost("signup")]
        public async Task<ActionResult<int>> SignUpAsync(SignUpDto signUpDto)
        {
            var id = await _service.SignUpAsync(signUpDto);
            return StatusCode(StatusCodes.Status201Created, id);
        }

        [Authorize]
        [HttpPut("password")]
        public async Task<ActionResult> UpdatePasswordAsync([FromBody] UpdatePasswordDto updatePasswordDto)
        {
            await _service.UpdatePasswordAsync(updatePasswordDto);
            return StatusCode(StatusCodes.Status202Accepted);
        }

        [Authorize]
        [HttpGet("token")]
        public async Task<ActionResult> IsTokenValid()
        {
            await _service.IsTokenValid();

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
