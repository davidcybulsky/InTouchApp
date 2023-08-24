using InTouchApi.Domain.Entities;

namespace InTouchApi.Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<User> GetAccountAsync(int id);
        Task UpdateAccountAsync(User account);
    }
}
