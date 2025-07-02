using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class ReplyCommentEntityTypeConfiguration : IEntityTypeConfiguration<ReplyComment>
    {
        public void Configure(EntityTypeBuilder<ReplyComment> builder)
        {
            builder.ToTable("ReplyComments");
            builder.HasKey(rc => rc.Id);

            builder.Property(rc => rc.Content).HasMaxLength(1500);
            builder.Property(rc => rc.MediaUrl).HasMaxLength(2048);
            builder.Property(rc => rc.MediaType).HasConversion<int>().IsRequired(false);
            builder.Property(rc => rc.CreatedBy).IsRequired();
            builder.Property(rc => rc.IsDeleted).HasDefaultValue(false);

            builder.HasOne(rc => rc.Replyer)
                   .WithMany(mu => mu.Replies)
                   .HasForeignKey(rc => rc.ReplyerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rc => rc.Comment)
                   .WithMany(pc => pc.Replies)
                   .HasForeignKey(rc => rc.CommentId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired(false);

            builder.HasOne(rc => rc.ParentReply)
                   .WithMany(pr => pr.Replies)
                   .HasForeignKey(rc => rc.ParentReplyId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired(false);

            builder.HasMany(rc => rc.Reactions)
                   .WithOne(rr => rr.ReplyComment)
                   .HasForeignKey(rr => rr.ReplyCommentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
