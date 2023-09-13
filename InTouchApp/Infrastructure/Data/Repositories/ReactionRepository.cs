using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces.Reaction;
using InTouchApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InTouchApi.Infrastructure.Data.Repositories
{
    public class ReactionRepository : IReactionRepository
    {
        private readonly ApiContext _apiContext;

        public ReactionRepository(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task CreateCommentReactionAsync(CommentReaction reaction)
        {
            var doesReactionExist = await _apiContext.CommentReactions
                .Where(cr => cr.IsDeleted == false)
                .FirstOrDefaultAsync(cr => cr.UserId == reaction.UserId && cr.CommentId == reaction.CommentId) != null;

            var doesCommentExist = await _apiContext.PostComments
                .Where(c => c.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == reaction.CommentId) != null;

            if (!doesCommentExist)
            {
                throw new NotFoundException("The comment does not exist",
                    $"User with id: {reaction.UserId} tried to react to comment with id: {reaction.CommentId}, but comment was not found");
            }

            if (doesReactionExist)
            {
                throw new BadRequestException("You have already reacted to the comment",
                    $"User with id: {reaction.UserId} tried to react to comment with id: {reaction.CommentId}, but has already reacted");
            }

            await _apiContext.AddAsync(reaction);
            await _apiContext.SaveChangesAsync();
        }

        public async Task CreatePostReactionAsync(PostReaction reaction)
        {
            var doesReactionExist = await _apiContext.PostReactions
                .Where(pr => pr.IsDeleted == false)
                .FirstOrDefaultAsync(pr => pr.UserId == reaction.UserId && pr.PostId == reaction.PostId) != null;

            var doesCommentExist = await _apiContext.Posts
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(p => p.Id == reaction.PostId) != null;

            if (!doesCommentExist)
            {
                throw new NotFoundException("The post does not exist",
                    $"User with id: {reaction.UserId} tried to react to post with id: {reaction.PostId}, but has already reacted");
            }

            if (doesReactionExist)
            {
                throw new BadRequestException("You have already reacted to the post",
                    $"User with id: {reaction.UserId} tried to react to post with id: {reaction.PostId}, but has already reacted");
            }

            await _apiContext.AddAsync(reaction);
            await _apiContext.SaveChangesAsync();
        }

        public async Task DeleteCommentReactionAsync(CommentReaction reaction)
        {
            var reactionInDb = await _apiContext.CommentReactions
                .Where(r => r.IsDeleted == false)
                .FirstOrDefaultAsync(r => r.Id == reaction.Id)
                ?? throw new NotFoundException("The reaction was not found",
                $"User with id: {reaction.LastModifiedById} tried to delete comment reaction with id: {reaction.Id}, but reaction was not found");

            reactionInDb.IsDeleted = true;

            reactionInDb.LastModificationDate = DateTime.UtcNow;
            reactionInDb.LastModifiedById = reaction.LastModifiedById;

            await _apiContext.SaveChangesAsync();
        }

        public async Task DeletePostReactionAsync(PostReaction reaction)
        {
            var reactionInDb = await _apiContext.PostReactions
                .Where(r => r.IsDeleted == false)
                .FirstOrDefaultAsync(r => r.Id == reaction.Id)
                ?? throw new NotFoundException("The reaction was not found",
                $"User with id: {reaction.LastModifiedById} tried to delete post reaction with id: {reaction.Id}, but reaction was not found");

            reactionInDb.IsDeleted = true;

            reactionInDb.LastModificationDate = DateTime.UtcNow;
            reactionInDb.LastModifiedById = reaction.LastModifiedById;

            await _apiContext.SaveChangesAsync();
        }

        public async Task<CommentReaction> GetCommentReactionAsync(int reactionId)
        {
            var reaction = await _apiContext.CommentReactions
                .AsNoTracking()
                .Where(r => r.IsDeleted == false)
                .FirstOrDefaultAsync(r => r.Id == reactionId)
                ?? throw new NotFoundException("The reaction was not found",
                $"Comment reaction with id: {reactionId} was not found");

            return reaction;
        }

        public async Task<PostReaction> GetPostReactionAsync(int reactionId)
        {
            var reaction = await _apiContext.PostReactions
                .AsNoTracking()
                .Where(r => r.IsDeleted == false)
                .FirstOrDefaultAsync(r => r.Id == reactionId)
                ?? throw new NotFoundException("The reaction was not found",
                $"Post reaction with id: {reactionId} was not found");

            return reaction;
        }

        public async Task UpdateCommentReactionAsync(CommentReaction reaction)
        {
            var reactionInDb = await _apiContext.CommentReactions
                .Where(r => r.IsDeleted == false)
                .FirstOrDefaultAsync(r => r.Id == reaction.Id)
                ?? throw new NotFoundException("The reaction was not found",
                $"User with id: {reaction.LastModifiedById} tried to update comment reaction with id: {reaction.Id}, but reaction was not found");

            reactionInDb.ReactionType = reaction.ReactionType;

            reactionInDb.LastModificationDate = DateTime.UtcNow;
            reactionInDb.LastModifiedById = reaction.LastModifiedById;

            await _apiContext.SaveChangesAsync();
        }

        public async Task UpdatePostReactionAsync(PostReaction reaction)
        {
            var reactionInDb = await _apiContext.PostReactions
                .Where(r => r.IsDeleted == false)
                .FirstOrDefaultAsync(r => r.Id == reaction.Id)
                ?? throw new NotFoundException("The reaction was not found",
                $"User with id: {reaction.LastModifiedById} tried to update post reaction with id: {reaction.Id}, but reaction was not found");

            reactionInDb.ReactionType = reaction.ReactionType;

            reactionInDb.LastModificationDate = DateTime.UtcNow;
            reactionInDb.LastModifiedById = reaction.LastModifiedById;

            await _apiContext.SaveChangesAsync();
        }
    }
}
