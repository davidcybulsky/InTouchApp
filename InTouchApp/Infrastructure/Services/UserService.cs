using AutoMapper;
using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Constants;
using InTouchApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace InTouchApi.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserHttpContextService _userHttpContextService;

        public UserService(IUserRepository repository,
                           IMapper mapper,
                           IPasswordHasher<User> passwordHasher,
                           IUserHttpContextService userHttpContextService)
        {
            _passwordHasher = passwordHasher;
            _repository = repository;
            _mapper = mapper;
            _userHttpContextService = userHttpContextService;
        }

        public async Task<int> CreateUserAsync(CreateUserDto createUserDto)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                "Unauthorized user tried to create a user");

            var user = _mapper.Map<User>(createUserDto);

            var hashedPassword = _passwordHasher.HashPassword(user, createUserDto.Password);

            user.PasswordHash = hashedPassword;

            if (user.Role.ToUpper() == "ADMIN")
            {
                user.Role = ROLES.ADMIN;
            }
            else if (user.Role.ToUpper() == "USER")
            {
                user.Role = ROLES.USER;
            }
            else
            {
                throw new BadRequestException("Choose a valid role",
                    $"User with id: {userId} tried to create user with invalid role: {user.Role}");
            }

            user.CreationDate = DateTime.UtcNow;
            user.CreatedById = userId;

            var id = await _repository.CreateUserAsync(user);

            Log.Logger.Information($"User with id: {userId} created user with id: {id}");

            return id;
        }

        public async Task DeleteUserAsync(int id)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to delete user with id: {id}");

            var user = await _repository.GetUserByIdAsync(id);

            user.LastModifiedById = userId;

            await _repository.DeleteUserAsync(user);

            Log.Logger.Information($"User with id: {userId} deleted user with id: {id}");
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _repository.GetAllUsersAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            Log.Logger.Information($"List of users was returned. Count: {userDtos.Count()}");

            return userDtos;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(user);

            Log.Logger.Information($"User with id: {id} was returned");

            return userDto;
        }

        public async Task UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to update user with id: {id}");

            var user = _mapper.Map<User>(updateUserDto);
            user.Id = id;
            user.LastModifiedById = userId;

            await _repository.UpdateUserAsync(user);

            Log.Logger.Information($"User with id: {userId} updated user with id: {id}");

        }

        public async Task UpdateUserRoleAsync(int id, UpdateRoleDto updateRoleDto)
        {

            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to change role for user with id: {id}");

            var user = await _repository.GetUserByIdAsync(id);

            if (updateRoleDto.Role.ToUpper() == "ADMIN")
            {
                user.Role = ROLES.ADMIN;
            }
            else if (updateRoleDto.Role.ToUpper() == "USER")
            {
                user.Role = ROLES.USER;
            }
            else
            {
                throw new BadRequestException("Choose a valid role",
                    $"User with id: {userId} tried to change role for user with id: {user.Id}, but role was invalid: {updateRoleDto.Role}");
            }

            user.LastModifiedById = userId;

            await _repository.UpdateUserAsync(user);

            Log.Logger.Information($"User with id: {userId} changed role of user with id: {id} to role: {user.Role}");

        }
    }
}
