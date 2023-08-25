using InTouchApi.Application.Models;

namespace InTouchApi.Application.Interfaces.Reaction
{
    public interface IReactionService
    {
        Task<int> CreatePostReactionAsync(int id, CreateReactionDto createReactionDto);
        Task UpdatePostReactionAsync(int id, UpdateReactionDto updateReactionDto);
        Task DeletePostReactionAsync(int id);
        Task<int> CreateCommentReactionAsync(int id, CreateReactionDto createReactionDto);
        Task UpdateCommentReactionAsync(int id, UpdateReactionDto updateReactionDto);
        Task DeleteCommentReactionAsync(int id);
    }
}
