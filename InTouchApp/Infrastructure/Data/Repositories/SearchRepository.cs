using InTouchApi.Application.Interfaces;
using InTouchApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InTouchApi.Infrastructure.Data.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly ApiContext _apiContext;

        public SearchRepository(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task<IEnumerable<Post>> GetPostsAsync(string pattern)
        {
            var posts1 = await _apiContext.Posts
                .AsNoTracking()
                .Where(p => p.Title.Contains(pattern))
                .ToListAsync();

            var posts2 = await _apiContext.Posts
                .AsNoTracking()
                .Where(p => p.Content.Contains(pattern))
                .ToListAsync();

            var posts = posts1.Union(posts2);

            return posts;
        }

        public async Task<IEnumerable<User>> GetUsersAsync(string pattern)
        {
            var users1 = await _apiContext.Users
                .AsNoTracking()
                .Where(u => u.FirstName.Contains(pattern))
                .ToListAsync();

            var users2 = await _apiContext.Users
                .AsNoTracking()
                .Where(u => u.LastName.Contains(pattern))
                .ToListAsync();

            var users = users1.Union(users2).ToList();

            return users;
        }
    }
}
