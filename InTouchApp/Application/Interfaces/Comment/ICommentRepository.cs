using InTouchApi.Domain.Entities;

namespace InTouchApi.Application.Interfaces
{
    public interface ICommentRepository
    {
        Task<PostComment> GetPostCommentByIdAsync(int id);
        Task<int> CreatePostCommentAsync(PostComment comment);
        Task UpdatePostCommentAsync(PostComment comment);
        Task DeletePostCommentAsync(PostComment comment);
    }
}
