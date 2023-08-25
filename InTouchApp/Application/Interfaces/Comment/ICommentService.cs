using InTouchApi.Application.Models;

namespace InTouchApi.Application.Interfaces
{
    public interface ICommentService
    {
        Task<int> CreatePostCommentAsync(int postId, CreateCommentDto createCommentDto);
        Task UpdatePostCommentAsync(int id, UpdateCommentDto updateCommentDto);
        Task DeletePostCommentAsync(int id);
    }
}
