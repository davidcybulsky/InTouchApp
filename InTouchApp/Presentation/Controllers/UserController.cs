using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
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
            return StatusCode(StatusCodes.Status200OK, userDtos);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>> GetUserByIdAsync([FromRoute] int userId)
        {
            var userDto = await _service.GetUserByIdAsync(userId);
            return StatusCode(StatusCodes.Status200OK, userDto);
        }

        [Authorize(Roles = ROLES.ADMIN)]
        [HttpPost]
        public async Task<ActionResult<int>> CreateUserAsync([FromBody] CreateUserDto createUserDto)
        {
            var id = await _service.CreateUserAsync(createUserDto);
            return StatusCode(StatusCodes.Status201Created, id);
        }

        [Authorize(Roles = ROLES.ADMIN)]
        [HttpPut("{userId}")]
        public async Task<ActionResult> UpdateUserAsync([FromRoute] int userId, [FromBody] UpdateUserDto updateUserDto)
        {
            await _service.UpdateUserAsync(userId, updateUserDto);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [Authorize(Roles = ROLES.ADMIN)]
        [HttpPut("{userId}/role")]
        public async Task<ActionResult> UpdateUserRoleAsync([FromRoute] int userId, [FromBody] UpdateRoleDto updateRoleDto)
        {
            await _service.UpdateUserRoleAsync(userId, updateRoleDto);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [Authorize(Roles = ROLES.ADMIN)]
        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUserAsync([FromRoute] int userId)
        {
            await _service.DeleteUserAsync(userId);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
