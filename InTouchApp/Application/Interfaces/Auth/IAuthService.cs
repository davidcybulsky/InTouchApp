using InTouchApi.Application.Models;

namespace InTouchApi.Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDto> LoginAsync(LoginDto loginDto);
        Task<int> SignUpAsync(SignUpDto signUpDto);
        Task UpdatePasswordAsync(UpdatePasswordDto updatePasswordDto);
        Task IsTokenValid();
    }
}