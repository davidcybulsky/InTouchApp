using InTouchApi.Domain.Entities;

namespace InTouchApi.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> GetLoginDataAsync(string email);
        Task<int> SignUpUserAsync(User user);
    }
}
