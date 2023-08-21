using AutoMapper;
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

        public AuthService(IAuthRepository repository, ITokenService tokenService, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _repository = repository;
            _tokenService = tokenService;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<TokenDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _repository.GetLoginDataAsync(loginDto.Email);
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
    }
}
