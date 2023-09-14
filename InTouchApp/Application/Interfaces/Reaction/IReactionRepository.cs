using InTouchApi.Domain.Entities;

namespace InTouchApi.Application.Interfaces.Reaction
{
    public interface IReactionRepository
    {
        Task<PostReaction> GetPostReactionAsync(int postId, int userId);
        Task CreatePostReactionAsync(PostReaction reaction);
        Task UpdatePostReactionAsync(PostReaction reaction);
        Task DeletePostReactionAsync(PostReaction reaction);


        Task<CommentReaction> GetCommentReactionAsync(int commentId, int userId);
        Task CreateCommentReactionAsync(CommentReaction reaction);
        Task UpdateCommentReactionAsync(CommentReaction reaction);
        Task DeleteCommentReactionAsync(CommentReaction reaction);
    }
}
