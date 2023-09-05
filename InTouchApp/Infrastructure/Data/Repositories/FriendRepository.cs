using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InTouchApi.Infrastructure.Data.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly ApiContext _apiContext;

        public FriendRepository(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task<Friendship> GetFriendshipAsync(int userId, int FriendId)
        {
            var friendship = await _apiContext.Friendships
                .AsNoTracking()
                .Where(f => f.IsDeleted == false)
                .FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == FriendId)
                ?? throw new NotFoundException("The friendship was not found");

            return friendship;
        }

        public async Task<IEnumerable<User>> GetUserFriendRequestsAsync(int userId)
        {
            var friendRequests = await _apiContext.Friendships
                .AsNoTracking()
                .Where(f => f.UserId == userId
                        && f.IsAccepted == false
                        && f.IsDeleted == false
                        && f.SendById != userId)
                .Include(f => f.Friend)
                .Select(f => f.Friend)
                .ToListAsync();
            return friendRequests;
        }

        public async Task<IEnumerable<User>> GetUserFriendsAsync(int userId)
        {
            var friends = await _apiContext.Friendships
                .AsNoTracking()
                .Where(f => f.UserId == userId
                        && f.IsAccepted == true
                        && f.IsDeleted == false)
                .Include(f => f.Friend)
                .Select(f => f.Friend)
                .ToListAsync();
            return friends;
        }

        public async Task<int> CreateFriendshipAsync(Friendship friendship)
        {
            await _apiContext.Friendships.AddAsync(friendship);
            await _apiContext.SaveChangesAsync();
            return friendship.Id;
        }

        public async Task UpdateFriendshipAsync(Friendship friendship)
        {
            var friendshipInDb = await _apiContext.Friendships
                .Where(f => f.IsDeleted == false)
                .FirstOrDefaultAsync(f => f.Id == friendship.Id)
                ?? throw new NotFoundException("The friendship was not found");

            friendshipInDb.IsAccepted = friendship.IsAccepted;

            friendshipInDb.LastModificationDate = DateTime.UtcNow;
            friendshipInDb.LastModifiedById = friendship.LastModifiedById;

            await _apiContext.SaveChangesAsync();
        }

        public async Task DeleteFriendshipAsync(Friendship friendship)
        {
            var friendshipInDb = await _apiContext.Friendships
                .Where(f => f.IsDeleted == false)
                .FirstOrDefaultAsync(f => f.Id == friendship.Id)
                ?? throw new NotFoundException("The friendship was not found");

            friendshipInDb.IsDeleted = true;

            friendshipInDb.LastModificationDate = DateTime.UtcNow;
            friendshipInDb.LastModifiedById = friendship.LastModifiedById;

            await _apiContext.SaveChangesAsync();
        }

        public async Task<bool> DoesFriendshipExist(int userId, int FriendId)
        {
            var friendship = await _apiContext.Friendships
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == FriendId);

            return friendship is not null;
        }

        public async Task<int> RecreateFriendshipAsync(Friendship friendship)
        {
            var friendshipInDb = await _apiContext.Friendships
                .FirstOrDefaultAsync(f => f.UserId == friendship.UserId
                                    && f.FriendId == friendship.FriendId)
                ?? throw new NotFoundException("The friendship does not exist");

            friendshipInDb.IsDeleted = false;
            friendshipInDb.IsAccepted = false;
            friendshipInDb.SendById = friendship.SendById;
            friendshipInDb.LastModificationDate = DateTime.UtcNow;
            friendshipInDb.LastModifiedById = friendship.LastModifiedById;

            await _apiContext.SaveChangesAsync();

            return friendshipInDb.Id;
        }
    }
}
