using InTouchApi.Application.Models;

namespace InTouchApi.Application.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDto> GetAccountAsync();
        Task UpdateAccountAsync(UpdateAccountDto updateAccountDto);
        Task DeleteAccountAsync();
    }
}
