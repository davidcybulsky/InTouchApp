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

        public async Task<int> CreatePostAsync(Post post)
        {
            await _dbContext.AddAsync(post);
            await _dbContext.SaveChangesAsync();
            return post.Id;
        }

        public async Task DeletePostAsync(Post post)
        {
            var postInDb = await _dbContext.Posts
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(p => p.Id == post.Id)
                ?? throw new NotFoundException("The post was not found");

            postInDb.IsDeleted = true;

            postInDb.LastModifiedById = post.LastModifiedById;
            post.LastModificationDate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            var posts = await _dbContext.Posts
                .AsNoTracking()
                .Include(p => p.Author)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Author)
                .Include(p => p.Comments)
                .ThenInclude(c => c.CommentReactions)
                .Where(p => p.IsDeleted == false)
                .ToListAsync();
            return posts;
        }

        public async Task<Post> GetPostAsTrackingAsync(int id)
        {
            var post = await _dbContext.Posts
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new NotFoundException("The user was not found");
            return post;
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            var post = await _dbContext.Posts
                .AsNoTracking()
                .Include(p => p.Author)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Author)
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new NotFoundException("The post was not found");
            return post;
        }

        public async Task UpdatePostAsync(Post post)
        {
            var postInDb = await _dbContext.Posts
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(p => p.Id == post.Id)
                ?? throw new NotFoundException("The post was not found");

            postInDb.Title = post.Title;
            postInDb.Content = post.Content;

            postInDb.LastModifiedById = post.LastModifiedById;
            post.LastModificationDate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }
    }
}
