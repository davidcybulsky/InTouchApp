using InTouchApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InTouchApi.Infrastructure.Data.Configuration
{
    public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasOne(f => f.Friend)
            .WithOne()
            .HasForeignKey<Friendship>(f => f.FriendId);
        }
    }
}
