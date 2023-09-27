using InTouchApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InTouchApi.Infrastructure.Data.Configuration
{
    public class PostCommentConfiguration : IEntityTypeConfiguration<PostComment>
    {
        public void Configure(EntityTypeBuilder<PostComment> builder)
        {
            builder.HasMany(c => c.CommentReactions)
            .WithOne()
            .HasForeignKey(r => r.CommentId);
        }
    }
}
