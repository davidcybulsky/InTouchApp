using AutoMapper;
using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Constants;
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
            var userId = _userHttpContextService.Id ?? throw new UnauthorizedException("");
            var user = _mapper.Map<User>(createUserDto);

            var hashedPassword = _passwordHasher.HashPassword(user, createUserDto.Password);

            user.PasswordHash = hashedPassword;

            if (user.Role == "ADMIN")
            {
                user.Role = ROLES.ADMIN;
            }
            else if (user.Role == "USER")
            {
                user.Role = ROLES.USER;
            }
            else
            {
                throw new BadRequestException("");
            }

            user.CreationDate = DateTime.UtcNow;
            user.CreatedById = userId;

            var id = await _repository.CreateUserAsync(user);

            return id;
        }

        public async Task DeleteUserAsync(int id)
        {
            var userId = _userHttpContextService.Id ?? throw new UnauthorizedException("");
            var user = await _repository.GetUserAsTrackingAsync(id);

            user.IsDeleted = true;

            user.LastModificationDate = DateTime.UtcNow;
            user.LastModifiedById = userId;

            await _repository.UpdateUserAsync();
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
            var userId = _userHttpContextService.Id ?? throw new UnauthorizedException("");
            var user = await _repository.GetUserAsTrackingAsync(id);
            user.Email = updateUserDto.Email;
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.BirthDate = updateUserDto.BirthDate;
            user.PhoneNumber = updateUserDto.PhoneNumber;
            user.Description = updateUserDto.Description;

            user.Role = updateUserDto.Role;

            user.FacebookURL = updateUserDto.FacebookURL;
            user.InstagramURL = updateUserDto.InstagramURL;
            user.LinkedInURL = updateUserDto.LinkedInURL;
            user.TikTokURL = updateUserDto.TikTokURL;
            user.YouTubeURL = updateUserDto.YouTubeURL;
            user.TwitterURL = updateUserDto.TwitterURL;

            user.Address.BuildingNumber = updateUserDto.Address.BuildingNumber;
            user.Address.LocalNumber = updateUserDto.Address.LocalNumber;
            user.Address.Street = updateUserDto.Address.Street;
            user.Address.ZipCode = updateUserDto.Address.ZipCode;
            user.Address.City = updateUserDto.Address.City;
            user.Address.Region = updateUserDto.Address.Region;
            user.Address.Country = updateUserDto.Address.Country;

            user.LastModificationDate = DateTime.UtcNow;
            user.LastModifiedById = userId;

            await _repository.UpdateUserAsync();
        }

        public async Task UpdateUserRoleAsync(int id, UpdateRoleDto updateRoleDto)
        {

            var userId = _userHttpContextService.Id ?? throw new UnauthorizedException("");
            var user = await _repository.GetUserAsTrackingAsync(id);

            if (updateRoleDto.Role == "ADMIN")
            {
                user.Role = ROLES.ADMIN;
            }
            else if (updateRoleDto.Role == "USER")
            {
                user.Role = ROLES.USER;
            }
            else
            {
                throw new BadRequestException("");
            }

            user.LastModificationDate = DateTime.UtcNow;
            user.LastModifiedById = userId;

            await _repository.UpdateUserAsync();
        }
    }
}
