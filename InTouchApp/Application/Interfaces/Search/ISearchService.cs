using InTouchApi.Application.Models;

namespace InTouchApi.Application.Interfaces
{
    public interface ISearchService
    {
        Task<IEnumerable<PostDto>> GetPostsAsync(string pattern);
        Task<IEnumerable<UserDto>> GetUsersAsync(string pattern);
    }
}
