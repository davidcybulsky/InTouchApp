using InTouchApi.Application.Interfaces;
using InTouchApi.Domain.Entities;

namespace InTouchApi.Infrastructure.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApiContext _apiContext;

        public CommentRepository(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public Task<int> CreatePostCommentAsync(PostComment comment)
        {
            throw new NotImplementedException();
        }

        public Task<PostComment> GetPostCommentAsTrackingAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PostComment> GetPostCommentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePostCommentAsync(PostComment comment)
        {
            throw new NotImplementedException();
        }
    }
}
