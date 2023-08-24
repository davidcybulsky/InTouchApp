using InTouchApi.Domain.Entities;

namespace InTouchApi.Application.Interfaces
{
    public interface IFriendRepository
    {
        Task<IEnumerable<User>> GetUserFriendsAsync(int userId);
        Task<IEnumerable<User>> GetUserFriendRequestsAsync(int userId);
        Task<Friendship> GetFriendshipAsync(int userId, int friendId);
        Task<int> CreateFriendshipAsync(Friendship friendship);
        Task UpdateFriendshipAsync(Friendship friendship);
    }
}
