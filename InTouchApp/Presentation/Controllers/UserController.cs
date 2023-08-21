using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace InTouchApi.Presentation.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var userDtos = await _service.GetAllUsersAsync();
            return StatusCode(200, userDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserByIdAsync([FromRoute] int id)
        {
            var userDto = await _service.GetUserByIdAsync(id);
            return StatusCode(200, userDto);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateUserAsync([FromBody] CreateUserDto createUserDto)
        {
            var id = await _service.CreateUserAsync(createUserDto);
            return StatusCode(200, id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserAsync([FromRoute] int id, [FromBody] UpdateUserDto updateUserDto)
        {
            await _service.UpdateUserAsync(id, updateUserDto);
            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAsync([FromRoute] int id)
        {
            await _service.DeleteUserAsync(id);
            return StatusCode(200);
        }
    }
}
