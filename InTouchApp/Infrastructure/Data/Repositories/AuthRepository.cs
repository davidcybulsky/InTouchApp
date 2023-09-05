using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InTouchApi.Infrastructure.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApiContext _dbContext;

        public AuthRepository(ApiContext apiContext)
        {
            _dbContext = apiContext;
        }

        public async Task ChangePasswordAsync(int userId, string NewPasswordHash)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new NotFoundException("The user was not found");

            user.PasswordHash = NewPasswordHash;

            user.LastModificationDate = DateTime.UtcNow;
            user.LastModifiedById = user.Id;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetLoginDataAsync(string email)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ??
                throw new BadRequestException("Bad email or password");

            return user;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId) ??
                throw new NotFoundException("The user was not found");

            return user;

        }

        public async Task<int> SignUpUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);

            user.CreationDate = DateTime.UtcNow;
            user.CreatedById = user.Id;

            await _dbContext.SaveChangesAsync();

            return user.Id;
        }
    }
}