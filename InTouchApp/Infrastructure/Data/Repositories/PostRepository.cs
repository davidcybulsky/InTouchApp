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

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            var posts = await _dbContext.Posts.AsNoTracking().Include(p => p.Author)
                .Where(p => p.IsDeleted == false).ToListAsync();
            return posts;
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            var post = await _dbContext.Posts.AsNoTracking().Include(p => p.Author)
                .Where(p => p.IsDeleted == false).FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new NotFoundException("The post was not found");
            return post;
        }

        public async Task UpdatePostAsync(Post post)
        {
            var postInDb = await _dbContext.Posts
                .Where(p => p.IsDeleted == false).FirstOrDefaultAsync(p => p.Id == post.Id)
                ?? throw new BadRequestException("The post can not be updated");
            postInDb = post;
            await _dbContext.SaveChangesAsync();
        }
    }
}
