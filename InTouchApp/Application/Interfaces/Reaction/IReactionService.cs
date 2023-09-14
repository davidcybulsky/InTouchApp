using InTouchApi.Application.Models;

namespace InTouchApi.Application.Interfaces.Reaction
{
    public interface IReactionService
    {
        Task CreatePostReactionAsync(int postId, CreateReactionDto createReactionDto);
        Task UpdatePostReactionAsync(int postId, UpdateReactionDto updateReactionDto);
        Task DeletePostReactionAsync(int postId);

        Task CreateCommentReactionAsync(int commentId, CreateReactionDto createReactionDto);
        Task UpdateCommentReactionAsync(int commentId, UpdateReactionDto updateReactionDto);
        Task DeleteCommentReactionAsync(int commentId);
    }
}
