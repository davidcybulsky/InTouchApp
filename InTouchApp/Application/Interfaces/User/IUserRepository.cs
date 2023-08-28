using InTouchApi.Domain.Entities;

namespace InTouchApi.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserAsTrackingAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<int> CreateUserAsync(User user);
        Task UpdateUserAsync();
    }
}
