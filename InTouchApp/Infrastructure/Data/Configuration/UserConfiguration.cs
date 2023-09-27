using InTouchApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InTouchApi.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
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

            builder.HasMany(u => u.UserPhotos)
            .WithOne()
            .HasForeignKey(p => p.UserId);
        }
    }
}
