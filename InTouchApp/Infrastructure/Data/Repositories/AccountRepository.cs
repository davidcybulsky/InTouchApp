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

        public async Task<User> GetAccountAsTrackingAsync(int id)
        {
            var account = await _apiContext.Users.Where(u => u.IsDeleted == false).Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Id == id) ?? throw new NotFoundException("");
            return account;
        }

        public async Task<User> GetAccountAsync(int id)
        {
            var user = await _apiContext.Users.AsNoTracking().Where(u => u.IsDeleted == false)
                .Include(u => u.Address).Include(u => u.Posts)
                .FirstOrDefaultAsync(u => u.Id == id) ?? throw new NotFoundException("");
            return user;
        }

        public async Task UpdateAccountAsync()
        {
            await _apiContext.SaveChangesAsync();
        }
    }
}
