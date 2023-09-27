using InTouchApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InTouchApi.Infrastructure.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<CommentReaction> CommentReactions { get; set; }

        public DbSet<PostReaction> PostReactions { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostComment> PostComments { get; set; }

        public DbSet<Friendship> Friendships { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<UserPhoto> UsersPhoto { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
