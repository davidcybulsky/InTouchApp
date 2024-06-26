﻿using AutoMapper;
using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Constants;
using InTouchApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace InTouchApi.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserHttpContextService _userHttpContextService;
        private readonly IEmailService _emailService;

        public AuthService(IAuthRepository repository,
            ITokenService tokenService,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            IUserHttpContextService userHttpContextService,
            IEmailService emailService)
        {
            _repository = repository;
            _tokenService = tokenService;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _userHttpContextService = userHttpContextService;
            _emailService = emailService;
        }

        public async Task<TokenDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _repository.GetLoginDataAsync(loginDto.Email);

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Bad email or password", $"User with id: {user.Id} provided bad password");
            }

            var token = await _tokenService.CreateTokenAsync(user);

            Log.Logger.Information($"User with id: {user.Id}, received token {token}");

            return token;
        }

        public Task<int> SignUpAsync(SignUpDto signUpDto)
        {
            var user = _mapper.Map<User>(signUpDto);
            user.Role = ROLES.USER;
            user.PasswordHash = _passwordHasher.HashPassword(user, signUpDto.Password);

            var id = _repository.SignUpUserAsync(user);

            _emailService.SendEmailAsync(user.Email, "Account creation", "Congratulations! Your account was created.");

            Log.Logger.Information($"User with email {user.Email} signed up and received id: {id}");

            return id;
        }

        public async Task UpdatePasswordAsync(UpdatePasswordDto updatePasswordDto)
        {
            var id = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized", $"Unauthorized user tried to change password");

            var user = await _repository.GetUserByIdAsync(id) ??
                throw new NotFoundException("The user was not found",
                $"The user with id: {id} was not found. Executed in auth repository"); ;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, updatePasswordDto.CurrentPassword);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Bad password",
                    $"User with id: {id} tried to update its password, but old password was bad");
            }

            var newPasswordHash = _passwordHasher.HashPassword(user, updatePasswordDto.NewPassword);

            await _repository.ChangePasswordAsync(user.Id, newPasswordHash);

            Log.Logger.Information($"User with id {user.Id} changed its password");
        }

        public async Task IsTokenValid()
        {
            var userId = _userHttpContextService.Id ?? throw new UnauthorizedException("Invalid token", "User is unauthorized");

            var user = await _repository.GetUserByIdAsync(userId);

            if (user is null)
            {
                throw new UnauthorizedException("Invalid token", $"User with id: {userId} and given token does not exist");
            }
        }
    }
}
