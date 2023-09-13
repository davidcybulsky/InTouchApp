using InTouchApi.Application.Models;

namespace InTouchApi.Application.Interfaces.Reaction
{
    public interface IReactionService
    {
        Task CreatePostReactionAsync(int postId, CreateReactionDto createReactionDto);
        Task UpdatePostReactionAsync(int reactionId, UpdateReactionDto updateReactionDto);
        Task DeletePostReactionAsync(int reactionId);

        Task CreateCommentReactionAsync(int commentId, CreateReactionDto createReactionDto);
        Task UpdateCommentReactionAsync(int reactionId, UpdateReactionDto updateReactionDto);
        Task DeleteCommentReactionAsync(int reactionId);
    }
}
