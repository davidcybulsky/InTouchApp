using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
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
            return StatusCode(200, tokenDto);
        }

        [HttpPost("signup")]
        public async Task<ActionResult<int>> SignUpAsync(SignUpDto signUpDto)
        {
            var id = await _service.SignUpAsync(signUpDto);
            return StatusCode(200, id);
        }
    }
}
