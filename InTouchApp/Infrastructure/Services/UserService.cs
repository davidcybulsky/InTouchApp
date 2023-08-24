using AutoMapper;
using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;

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
            var user = _mapper.Map<User>(createUserDto);
            var hashedPassword = _passwordHasher.HashPassword(user, createUserDto.Password);
            user.PasswordHash = hashedPassword;
            var id = await _repository.CreateUserAsync(user);
            return id;
        }

        public async Task DeleteUserAsync(int id)
        {
            await _repository.DeleteUserAsync(id);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _repository.GetAllUsersAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return userDtos;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            var user = _mapper.Map<User>(updateUserDto);
            var userInDb = await _repository.GetUserByIdAsync(id);
            user.Id = id;
            user.CreatedById = userInDb.CreatedById;
            user.CreationDate = userInDb.CreationDate;
            user.LastModificationDate = DateTime.UtcNow;
            user.LastModifiedById = _userHttpContextService.Id ?? throw new UnauthorizedException("");
            user.PasswordHash = userInDb.PasswordHash;

            await _repository.UpdateUserAsync(user);
        }
    }
}
