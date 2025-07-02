using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class PostCommentEntityTypeConfiguration : IEntityTypeConfiguration<PostComment>
    {
        public void Configure(EntityTypeBuilder<PostComment> builder)
        {
            builder.ToTable("PostComments");
            builder.HasKey(pc => pc.Id);

            builder.Property(pc => pc.Content).HasMaxLength(1500);
            builder.Property(pc => pc.MediaUrl).HasMaxLength(2048);
            builder.Property(pc => pc.MediaType).HasConversion<int>().IsRequired(false);
            builder.Property(pc => pc.CreatedBy).IsRequired();
            builder.Property(pc => pc.IsDeleted).HasDefaultValue(false);

            builder.HasOne(pc => pc.Commenter)
                   .WithMany(mu => mu.Comments)
                   .HasForeignKey(pc => pc.CommenterId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pc => pc.Post)
                   .WithMany(p => p.PostComments)
                   .HasForeignKey(pc => pc.PostId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pc => pc.Reactions)
                   .WithOne(cr => cr.Comment)
                   .HasForeignKey(cr => cr.CommentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pc => pc.Replies)
                   .WithOne(rc => rc.Comment)
                   .HasForeignKey(rc => rc.CommentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}