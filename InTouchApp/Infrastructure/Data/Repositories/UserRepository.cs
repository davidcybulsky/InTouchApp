using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InTouchApi.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiContext _dbcontext;

        public UserRepository(ApiContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<int> CreateUserAsync(User user)
        {
            await _dbcontext.AddAsync(user);
            await _dbcontext.SaveChangesAsync();
            return user.Id;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _dbcontext.Users.AsNoTracking()
                .Where(u => u.IsDeleted == false).ToListAsync();

            return users;
        }

        public async Task<User> GetUserAsTrackingAsync(int id)
        {
            var user = await _dbcontext.Users.Where(u => u.IsDeleted == false)
                .FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("");
            return user;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _dbcontext.Users.AsNoTracking().
                Where(u => u.IsDeleted == false).Include(u => u.Posts).FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("The user was not found");
            return user;
        }

        public async Task UpdateUserAsync()
        {
            await _dbcontext.SaveChangesAsync();
        }
    }
}
