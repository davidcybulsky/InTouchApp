using InTouchApi.Domain.Entities;

namespace InTouchApi.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> GetLoginDataAsync(string email);
        Task<int> SignUpUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
        Task ChangePasswordAsync(int id, string NewPasswordHash);
    }
}
