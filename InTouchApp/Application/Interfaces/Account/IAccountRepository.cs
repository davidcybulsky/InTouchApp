using InTouchApi.Domain.Entities;

namespace InTouchApi.Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<User> GetAccountAsync(int id);
        Task<User> GetAccountAsTrackingAsync(int id);
        Task UpdateAccountAsync();
    }
}
