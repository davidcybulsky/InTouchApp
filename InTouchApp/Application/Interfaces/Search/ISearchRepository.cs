using InTouchApi.Domain.Entities;

namespace InTouchApi.Application.Interfaces
{
    public interface ISearchRepository
    {
        Task<IEnumerable<User>> GetUsersAsync(string pattern);
        Task<IEnumerable<Post>> GetPostsAsync(string pattern);
    }
}
