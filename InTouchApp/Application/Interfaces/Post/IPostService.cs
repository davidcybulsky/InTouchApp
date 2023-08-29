using InTouchApi.Application.Models;

namespace InTouchApi.Application.Interfaces
{
    public interface IPostService
    {
        Task<PostDto> GetPostByIdAsync(int id);
        Task<IEnumerable<PostDto>> GetAllPostsAsync();
        Task<IEnumerable<PostDto>> GetUserPostsAsync(int id);
        Task<int> CreatePostAsync(CreatePostDto createPostDto);
        Task UpdatePostAsync(int id, UpdatePostDto updatePostDto);
        Task DeletePostAsync(int id);
    }
}
