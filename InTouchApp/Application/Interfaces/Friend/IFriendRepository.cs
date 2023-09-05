using InTouchApi.Domain.Entities;

namespace InTouchApi.Application.Interfaces
{
    public interface IFriendRepository
    {
        Task<IEnumerable<User>> GetUserFriendsAsync(int userId);
        Task<IEnumerable<User>> GetUserFriendRequestsAsync(int userId);
        Task<Friendship> GetFriendshipAsync(int userId, int FriendId);
        Task<int> CreateFriendshipAsync(Friendship friendship);
        Task UpdateFriendshipAsync(Friendship friendship);
        Task DeleteFriendshipAsync(Friendship friendship);
        Task<bool> DoesFriendshipExist(int userId, int FriendId);
        Task<int> RecreateFriendshipAsync(Friendship friendship);
    }
}
