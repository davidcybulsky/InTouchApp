using InTouchApi.Application.Interfaces.Reaction;
using InTouchApi.Application.Models;

namespace InTouchApi.Infrastructure.Services
{
    public class ReactionService : IReactionService
    {
        public Task<int> CreateCommentReactionAsync(int id, CreateReactionDto createReactionDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreatePostReactionAsync(int id, CreateReactionDto createReactionDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCommentReactionAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeletePostReactionAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCommentReactionAsync(int id, UpdateReactionDto updateReactionDto)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePostReactionAsync(int id, UpdateReactionDto updateReactionDto)
        {
            throw new NotImplementedException();
        }
    }
}
