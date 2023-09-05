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

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _dbcontext.Users
                .AsNoTracking()
                .Where(u => u.IsDeleted == false)
                .Include(u => u.Address)
                .Include(u => u.Posts)
                .ToListAsync();

            return users;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _dbcontext.Users
                .AsNoTracking()
                .Where(u => u.IsDeleted == false)
                .Include(u => u.Address)
                .Include(u => u.Posts)
                .FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("The user was not found");

            return user;
        }

        public async Task<int> CreateUserAsync(User user)
        {
            await _dbcontext.AddAsync(user);
            await _dbcontext.SaveChangesAsync();

            return user.Id;
        }

        public async Task UpdateUserAsync(User user)
        {
            var userInDb = await _dbcontext.Users
                .Where(u => u.IsDeleted == false)
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Id == user.Id)
                ?? throw new NotFoundException("The user was not found");

            userInDb.Email = user.Email;
            userInDb.FirstName = user.FirstName;
            userInDb.LastName = user.LastName;
            userInDb.BirthDate = user.BirthDate;
            userInDb.PhoneNumber = user.PhoneNumber;
            userInDb.Description = user.Description;
            userInDb.Role = user.Role;

            userInDb.FacebookURL = user.FacebookURL;
            userInDb.InstagramURL = user.InstagramURL;
            userInDb.LinkedInURL = user.LinkedInURL;
            userInDb.TikTokURL = user.TikTokURL;
            userInDb.YouTubeURL = user.YouTubeURL;
            userInDb.TwitterURL = user.TwitterURL;

            userInDb.Address.BuildingNumber = user.Address.BuildingNumber;
            userInDb.Address.LocalNumber = user.Address.LocalNumber;
            userInDb.Address.Street = user.Address.Street;
            userInDb.Address.ZipCode = user.Address.ZipCode;
            userInDb.Address.City = user.Address.City;
            userInDb.Address.Region = user.Address.Region;
            userInDb.Address.Country = user.Address.Country;

            userInDb.LastModificationDate = DateTime.UtcNow;
            userInDb.LastModifiedById = user.LastModifiedById;

            userInDb.Address.LastModifiedById = user.LastModifiedById;
            userInDb.Address.LastModificationDate = DateTime.UtcNow;

            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            var userInDb = await _dbcontext.Users
                .Where(u => u.IsDeleted == false)
                .FirstOrDefaultAsync(u => u.Id == user.Id)
                ?? throw new NotFoundException("The user was not found");

            userInDb.IsDeleted = true;
            userInDb.LastModificationDate = DateTime.UtcNow;
            userInDb.LastModifiedById = user.LastModifiedById;

            await _dbcontext.SaveChangesAsync();
        }
    }
}
