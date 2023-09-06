using InTouchApi.Domain.Entities;

namespace InTouchApi.Application.Interfaces.Reaction
{
    public interface IReactionRepository
    {
        Task<PostReaction> GetPostReactionAsync(int reactionId);
        Task<int> CreatePostReactionAsync(PostReaction reaction);
        Task UpdatePostReactionAsync(PostReaction reaction);
        Task DeletePostReactionAsync(PostReaction reaction);


        Task<CommentReaction> GetCommentReactionAsync(int reactionId);
        Task<int> CreateCommentReactionAsync(CommentReaction reaction);
        Task UpdateCommentReactionAsync(CommentReaction reaction);
        Task DeleteCommentReactionAsync(CommentReaction commentReaction);
    }
}
