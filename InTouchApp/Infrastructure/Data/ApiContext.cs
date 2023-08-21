using InTouchApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(builder =>
            {
                builder.HasMany(u => u.Posts)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.AuthorId);

                builder.HasOne(u => u.Address)
                .WithOne()
                .HasForeignKey<Address>(a => a.UserId);

                builder.HasMany(u => u.Friends)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId);

                builder.HasMany(u => u.Comments)
                .WithOne(p => p.Author)
                .HasForeignKey(c => c.UserId);

                builder.HasMany(u => u.PostReactions)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.UserId);

                builder.HasMany(u => u.CommentReactions)
                .WithOne(p => p.Author)
                .HasForeignKey(c => c.UserId);
            });

            builder.Entity<Friendship>(builder =>
            {
                builder.HasOne(f => f.Friend)
                .WithOne()
                .HasForeignKey<Friendship>(f => f.FriendId);
            });

            builder.Entity<Post>(builder =>
            {
                builder.HasMany(p => p.Comments)
                .WithOne()
                .HasForeignKey(c => c.PostId);

                builder.HasMany(p => p.Reactions)
                .WithOne()
                .HasForeignKey(r => r.PostId);
            });

            builder.Entity<PostComment>(builder =>
            {
                builder.HasMany(c => c.CommentReactions)
                .WithOne()
                .HasForeignKey(r => r.CommentId);
            });
        }
    }
}
