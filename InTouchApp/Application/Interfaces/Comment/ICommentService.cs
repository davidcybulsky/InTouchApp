using InTouchApi.Application.Models;

namespace InTouchApi.Application.Interfaces
{
    public interface ICommentService
    {
        Task<IncludeCommentDto> CreatePostCommentAsync(int postId, CreateCommentDto createCommentDto);
        Task UpdatePostCommentAsync(int id, UpdateCommentDto updateCommentDto);
        Task DeletePostCommentAsync(int id);
    }
}
