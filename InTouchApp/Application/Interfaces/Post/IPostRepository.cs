using InTouchApi.Domain.Entities;

namespace InTouchApi.Application.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> GetPostByIdAsync(int id);
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> CreatePostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(Post post);
    }
}
