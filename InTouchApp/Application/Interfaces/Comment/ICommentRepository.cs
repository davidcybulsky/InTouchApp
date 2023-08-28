using InTouchApi.Domain.Entities;

namespace InTouchApi.Application.Interfaces
{
    public interface ICommentRepository
    {
        Task<PostComment> GetPostCommentAsync(int id);
        Task<PostComment> GetPostCommentAsTrackingAsync(int id);
        Task<int> CreatePostCommentAsync(PostComment comment);
        Task UpdatePostCommentAsync();
    }
}
