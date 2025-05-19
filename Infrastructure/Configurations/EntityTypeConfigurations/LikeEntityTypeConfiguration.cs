using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class LikeEntityTypeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.PostId);
            builder.Property(l => l.CommentId);
            builder.Property(l => l.ReplyId);
            builder.Property(l => l.LikerId);

            builder.HasOne(l => l.Liker)
               .WithMany(m => m.Likes)
               .HasForeignKey(l => l.LikerId);
        }
    }
}
