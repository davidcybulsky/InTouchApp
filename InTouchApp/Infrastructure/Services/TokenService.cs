using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Entities;

namespace InTouchApi.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        public Task<TokenDto> CreateTokenAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
