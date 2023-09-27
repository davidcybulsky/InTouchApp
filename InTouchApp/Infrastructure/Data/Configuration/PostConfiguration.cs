using InTouchApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InTouchApi.Infrastructure.Data.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasMany(p => p.Comments)
                .WithOne()
                .HasForeignKey(c => c.PostId);

            builder.HasMany(p => p.Reactions)
                .WithOne()
                .HasForeignKey(r => r.PostId);
        }
    }
}
