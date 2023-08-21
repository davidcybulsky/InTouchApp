using AutoMapper;
using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace InTouchApi.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserHttpContextService _userHttpContextService;

        public AuthService(IAuthRepository repository,
            ITokenService tokenService,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            IUserHttpContextService userHttpContextService)
        {
            _repository = repository;
            _tokenService = tokenService;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _userHttpContextService = userHttpContextService;
        }

        public async Task<TokenDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _repository.GetLoginDataAsync(loginDto.Email);

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Bad email or password");
            }

            var token = await _tokenService.CreateTokenAsync(user);
            return token;
        }

        public Task<int> SignUpAsync(SignUpDto signUpDto)
        {
            var user = _mapper.Map<User>(signUpDto);
            user.PasswordHash = _passwordHasher.HashPassword(user, signUpDto.Password);
            var id = _repository.SignUpUserAsync(user);
            return id;
        }

        public async Task UpdatePasswordAsync(UpdatePasswordDto updatePasswordDto)
        {
            var id = _userHttpContextService.Id ?? throw new Exception();
            var user = await _repository.GetUserByIdAsync(id);

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, updatePasswordDto.CurrentPassword);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Bad password");
            }

            var newPasswordHash = _passwordHasher.HashPassword(user, updatePasswordDto.NewPassword);

            await _repository.ChangePasswordAsync(user.Id, newPasswordHash);
        }
    }
}
