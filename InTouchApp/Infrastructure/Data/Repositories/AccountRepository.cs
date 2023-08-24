using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InTouchApi.Infrastructure.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApiContext _apiContext;

        public AccountRepository(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task<User> GetAccountAsync(int id)
        {
            var user = await _apiContext.Users.Where(u => u.IsDeleted == false)
                .FirstOrDefaultAsync(u => u.Id == id) ?? throw new NotFoundException("");
            return user;
        }

        public async Task UpdateAccountAsync(User account)
        {
            var userInDb = await _apiContext.Users.Where(u => u.IsDeleted == false)
                .FirstOrDefaultAsync(u => u.Id == account.Id) ?? throw new NotFoundException("");
            userInDb = account;
            await _apiContext.SaveChangesAsync();
        }
    }
}
