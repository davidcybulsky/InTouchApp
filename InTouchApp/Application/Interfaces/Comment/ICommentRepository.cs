using InTouchApi.Domain.Entities;

namespace InTouchApi.Application.Interfaces
{
    public interface ICommentRepository
    {
        Task<PostComment> GetPostCommentByIdAsync(int id);
        Task<PostComment> CreatePostCommentAsync(PostComment comment);
        Task UpdatePostCommentAsync(PostComment comment);
        Task DeletePostCommentAsync(PostComment comment);
    }
}
