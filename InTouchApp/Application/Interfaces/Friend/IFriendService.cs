using InTouchApi.Application.Models;

namespace InTouchApi.Application.Interfaces
{
    public interface IFriendService
    {
        Task<IEnumerable<FriendDto>> GetFriendsAsync();
        Task<IEnumerable<FriendDto>> GetFriendRequestsAsync();
        Task<IEnumerable<FriendDto>> GetUserFriendsAsync(int id);
        Task<IEnumerable<FriendDto>> GetUserFriendRequestsAsync(int id);
        Task SendFriendRequestAsync(int friendId);
        Task AcceptFriendRequestAsync(int friendId);
    }
}
