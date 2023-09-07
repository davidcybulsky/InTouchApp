using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InTouchApi.Infrastructure.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApiContext _apiContext;

        public CommentRepository(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task<int> CreatePostCommentAsync(PostComment comment)
        {
            var doesPostExist = (await _apiContext.Posts
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(p => p.Id == comment.PostId)) != null;

            if (!doesPostExist)
            {
                throw new NotFoundException("The post was not found",
                    $"User with id: {comment.CreatedById} tried to create comment for post with id: {comment.PostId}, but post was not found");
            }

            await _apiContext.AddAsync(comment);
            await _apiContext.SaveChangesAsync();
            return comment.Id;
        }

        public async Task DeletePostCommentAsync(PostComment comment)
        {
            var postComment = await _apiContext.PostComments
                .Where(c => c.IsDeleted == false)
                .FirstOrDefaultAsync(p => p.Id == comment.Id)
                ?? throw new NotFoundException("The comment was not found",
                $"User with id: {comment.LastModifiedById} tried to deleted comment with id: {comment.Id}, but it was not found");

            postComment.IsDeleted = true;

            postComment.LastModifiedById = comment.LastModifiedById;
            postComment.LastModificationDate = DateTime.UtcNow;

            var commentReactions = await _apiContext.CommentReactions
                .Where(cr => cr.IsDeleted == false && cr.CommentId == comment.Id)
                .ToListAsync();

            foreach (var reaction in commentReactions)
            {
                reaction.IsDeleted = true;
                reaction.LastModifiedById = comment.LastModifiedById;
                reaction.LastModificationDate = DateTime.UtcNow;
            }

            await _apiContext.SaveChangesAsync();
        }

        public async Task<PostComment> GetPostCommentByIdAsync(int id)
        {
            var postComment = await _apiContext.PostComments
                .Where(c => c.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == id)
                ?? throw new NotFoundException("The comment was not found",
                $"Comment with id: {id} was not found");
            return postComment;
        }

        public async Task UpdatePostCommentAsync(PostComment comment)
        {
            var postComment = await _apiContext.PostComments
                .Where(c => c.IsDeleted == false)
                .FirstOrDefaultAsync(p => p.Id == comment.Id)
                ?? throw new NotFoundException("The comment was not found",
                $"User with id: {comment.LastModifiedById} tried to update comment with id: {comment.Id}, but it was not found");

            postComment.Content = comment.Content;

            postComment.LastModifiedById = comment.LastModifiedById;
            postComment.LastModificationDate = DateTime.UtcNow;

            await _apiContext.SaveChangesAsync();
        }
    }
}
