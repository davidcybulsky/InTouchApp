using InTouchApi.Domain.Entities;

namespace InTouchApi.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> GetLoginDataAsync(string email);
        Task<int> SignUpUserAsync(User user);
        Task<User> GetUserByIdAsync(int userId);
        Task ChangePasswordAsync(int userId, string NewPasswordHash);
    }
}
