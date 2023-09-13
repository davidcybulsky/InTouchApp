using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InTouchApi.Infrastructure.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApiContext _dbContext;

        public PostRepository(ApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            await _dbContext.AddAsync(post);
            await _dbContext.SaveChangesAsync();
            return post;
        }

        public async Task DeletePostAsync(Post post)
        {
            var postInDb = await _dbContext.Posts
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(p => p.Id == post.Id)
                ?? throw new NotFoundException("The post was not found",
                $"User with id: {post.LastModifiedById} tried to delete post with id: {post.Id}, but it was not found");

            postInDb.IsDeleted = true;
            postInDb.LastModifiedById = post.LastModifiedById;
            postInDb.LastModificationDate = DateTime.UtcNow;

            var postComments = await _dbContext.PostComments
                .Where(pc => pc.IsDeleted == false && pc.PostId == post.Id)
                .ToListAsync();

            foreach (var postComment in postComments)
            {
                postComment.IsDeleted = true;
                postComment.LastModifiedById = post.LastModifiedById;
                postComment.LastModificationDate = DateTime.UtcNow;
            }

            var postReactions = await _dbContext.PostReactions
                .Where(pr => pr.IsDeleted == false && pr.PostId == post.Id)
                .ToListAsync();

            await _dbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            var posts = await _dbContext.Posts
                .AsNoTracking()
                .Include(p => p.Author)
                .Include(p => p.Comments.Where(c => c.IsDeleted == false))
                .ThenInclude(c => c.Author)
                .Include(p => p.Comments.Where(c => c.IsDeleted == false))
                .ThenInclude(c => c.CommentReactions.Where(c => c.IsDeleted == false))
                .ThenInclude(cr => cr.Author)
                .Include(p => p.Reactions.Where(r => r.IsDeleted == false))
                .ThenInclude(r => r.Author)
                .Where(p => p.IsDeleted == false)
                .ToListAsync();

            return posts;
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            var post = await _dbContext.Posts
               .AsNoTracking()
                .Include(p => p.Author)
                .Include(p => p.Comments.Where(c => c.IsDeleted == false))
                .ThenInclude(c => c.Author)
                .Include(p => p.Comments.Where(c => c.IsDeleted == false))
                .ThenInclude(c => c.CommentReactions.Where(c => c.IsDeleted == false))
                .Include(p => p.Reactions.Where(r => r.IsDeleted == false))
                .ThenInclude(r => r.Author)
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new NotFoundException("The post was not found",
                $"Post with id: {id} was not found");
            return post;
        }

        public async Task UpdatePostAsync(Post post)
        {
            var postInDb = await _dbContext.Posts
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(p => p.Id == post.Id)
                ?? throw new NotFoundException("The post was not found",
                $"User with id: {post.LastModifiedById} tried to update post with id: {post.Id}, but post was not found");

            postInDb.Title = post.Title;
            postInDb.Content = post.Content;

            postInDb.LastModifiedById = post.LastModifiedById;
            post.LastModificationDate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }
    }
}
