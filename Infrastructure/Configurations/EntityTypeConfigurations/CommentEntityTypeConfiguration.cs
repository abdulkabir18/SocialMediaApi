using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Text).IsRequired();
            builder.Property(c => c.PostId).IsRequired();
            builder.Property(c => c.CommenterId).IsRequired();

            builder.HasMany(c => c.Replies)
               .WithOne(r => r.Comment)
               .HasForeignKey(r => r.CommentId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Likes)
                   .WithOne(l => l.Comment)
                   .HasForeignKey(l => l.CommentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(c => c.IsDeleted);
        }
    }
}
