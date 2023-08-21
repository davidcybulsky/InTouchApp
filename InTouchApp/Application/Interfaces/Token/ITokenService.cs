using InTouchApi.Application.Models;
using InTouchApi.Domain.Entities;

namespace InTouchApi.Application.Interfaces
{
    public interface ITokenService
    {
        Task<TokenDto> CreateTokenAsync(User user);
    }
}
