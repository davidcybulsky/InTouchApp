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

        public async Task<int> CreateFriendshipAsync(Friendship friendship)
        {
            await _apiContext.Friendships.AddAsync(friendship);
            await _apiContext.SaveChangesAsync();
            return friendship.Id;
        }

        public async Task<Friendship> GetFriendshipAsTrackingAsync(int userId, int friendId)
        {
            var friendship = await _apiContext.Friendships
                .Where(f => f.IsDeleted == false)
                .FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == friendId)
                ?? throw new NotFoundException("The friendship was not found");
            return friendship;
        }

        public async Task<IEnumerable<User>> GetUserFriendRequestsAsync(int userId)
        {
            var friendRequests = await _apiContext.Friendships.AsNoTracking()
                .Where(f => f.UserId == userId && f.IsAccepted == false)
                .Include(f => f.Friend).Select(f => f.Friend).ToListAsync();
            return friendRequests;
        }

        public async Task<IEnumerable<User>> GetUserFriendsAsync(int userId)
        {
            var friends = await _apiContext.Friendships.AsNoTracking()
                .Where(f => f.UserId == userId && f.IsAccepted == true)
                .Include(f => f.Friend).Select(f => f.Friend).ToListAsync();
            return friends;
        }

        public async Task UpdateFriendshipAsync()
        {
            await _apiContext.SaveChangesAsync();
        }
    }
}
