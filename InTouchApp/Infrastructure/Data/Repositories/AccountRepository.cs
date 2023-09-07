using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InTouchApi.Infrastructure.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApiContext _apiContext;

        public AccountRepository(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task<User> GetAccountAsync(int id)
        {
            var user = await _apiContext.Users
                .AsNoTracking()
                .Where(u => u.IsDeleted == false)
                .Include(u => u.Address)
                .Include(u => u.Posts.Where(c => c.IsDeleted == false))
                .FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("Your account was not found",
                    $"User with id: {id} tried to get its account, but it was not found");
            return user;
        }

        public async Task UpdateAccountAsync(User account)
        {
            var user = await _apiContext.Users
                .Where(u => u.IsDeleted == false)
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Id == account.Id)
                ?? throw new NotFoundException("Your account was not found",
                 $"User with id: {account.Id} tried to update its account, but it was not found");

            user.FirstName = account.FirstName;
            user.LastName = account.LastName;
            user.PhoneNumber = account.PhoneNumber;
            user.Description = account.Description;

            user.FacebookURL = account.FacebookURL;
            user.InstagramURL = account.InstagramURL;
            user.LinkedInURL = account.LinkedInURL;
            user.TikTokURL = account.TikTokURL;
            user.YouTubeURL = account.YouTubeURL;
            user.TwitterURL = account.TwitterURL;

            user.Address.LocalNumber = account.Address.LocalNumber;
            user.Address.BuildingNumber = account.Address.BuildingNumber;
            user.Address.Street = account.Address.Street;
            user.Address.City = account.Address.City;
            user.Address.ZipCode = account.Address.ZipCode;
            user.Address.Region = account.Address.Region;
            user.Address.Country = account.Address.Country;

            user.LastModificationDate = DateTime.UtcNow;
            user.LastModifiedById = user.Id;

            user.Address.LastModificationDate = DateTime.UtcNow;
            user.Address.LastModifiedById = user.Id;

            await _apiContext.SaveChangesAsync();
        }


        public async Task DeleteAccountAsync(User account)
        {
            var user = await _apiContext.Users
                .Where(u => u.IsDeleted == false)
                .FirstOrDefaultAsync(u => u.Id == account.Id)
                ?? throw new NotFoundException("Your account was not found",
                 $"User with id: {account.Id} tried to delete its account, but it was not found");

            user.IsDeleted = true;

            user.LastModificationDate = DateTime.UtcNow;
            user.LastModifiedById = user.Id;

            var posts = await _apiContext.Posts
                .Where(u => u.IsDeleted == false && u.AuthorId == user.Id)
                .ToListAsync();

            foreach (var post in posts)
            {
                post.IsDeleted = true;
                post.LastModifiedById = user.Id;
                post.LastModificationDate = DateTime.UtcNow;
            }

            var postComments = await _apiContext.PostComments
                .Where(u => u.IsDeleted == false && u.UserId == user.Id)
                .ToListAsync();

            foreach (var postComment in postComments)
            {
                postComment.IsDeleted = true;
                postComment.LastModifiedById = user.Id;
                postComment.LastModificationDate = DateTime.UtcNow;
            }

            var friendships = _apiContext.Friendships
                .Where(f => f.IsDeleted == false
                        && (f.FriendId == user.Id || f.UserId == user.Id)
                        ).ToList();

            foreach (var friendship in friendships)
            {
                friendship.IsDeleted = true;
                friendship.LastModifiedById = user.Id;
                friendship.LastModificationDate = DateTime.UtcNow;
            }

            var postReactions = await _apiContext.PostReactions
                .Where(f => f.IsDeleted == false && f.UserId == user.Id)
                .ToListAsync();

            foreach (var postReaction in postReactions)
            {
                postReaction.IsDeleted = true;
                postReaction.LastModifiedById = user.Id;
                postReaction.LastModificationDate = DateTime.UtcNow;
            }

            var commentReactions = await _apiContext.CommentReactions
                .Where(f => f.IsDeleted == false && f.UserId == user.Id)
                .ToListAsync();

            foreach (var commentReaction in commentReactions)
            {
                commentReaction.IsDeleted = true;
                commentReaction.LastModifiedById = user.Id;
                commentReaction.LastModificationDate = DateTime.UtcNow;
            }

            await _apiContext.SaveChangesAsync();
        }
    }
}
