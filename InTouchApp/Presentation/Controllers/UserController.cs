﻿using InTouchApi.Application.Interfaces;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserByIdAsync([FromRoute] int id)
        {
            var userDto = await _service.GetUserByIdAsync(id);
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
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserAsync([FromRoute] int id, [FromBody] UpdateUserDto updateUserDto)
        {
            await _service.UpdateUserAsync(id, updateUserDto);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [Authorize(Roles = ROLES.ADMIN)]
        [HttpPut("{id}/role")]
        public async Task<ActionResult> UpdateUserRoleAsync([FromRoute] int id, [FromBody] UpdateRoleDto updateRoleDto)
        {
            await _service.UpdateUserRoleAsync(id, updateRoleDto);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [Authorize(Roles = ROLES.ADMIN)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAsync([FromRoute] int id)
        {
            await _service.DeleteUserAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
