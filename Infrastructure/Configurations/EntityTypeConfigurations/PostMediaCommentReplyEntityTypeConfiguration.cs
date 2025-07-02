using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class PostMediaCommentReplyEntityTypeConfiguration : IEntityTypeConfiguration<PostMediaCommentReply>
    {
        public void Configure(EntityTypeBuilder<PostMediaCommentReply> builder)
        {
            builder.ToTable("PostMediaCommentReplies");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Content).HasMaxLength(1500);
            builder.Property(r => r.MediaUrl).HasMaxLength(2048);
            builder.Property(r => r.MediaType).HasConversion<int>().IsRequired(false);
            builder.Property(r => r.CreatedBy).IsRequired();
            builder.Property(r => r.IsDeleted).HasDefaultValue(false);

            builder.HasOne(r => r.Replyer)
                   .WithMany(mu => mu.PostMediaCommentReplies)
                   .HasForeignKey(r => r.ReplyerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Comment)
                   .WithMany(c => c.Replies)
                   .HasForeignKey(r => r.CommentId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired(false);

            builder.HasOne(r => r.ParentReply)
                   .WithMany(pr => pr.Replies)
                   .HasForeignKey(r => r.ParentReplyId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired(false);

            builder.HasMany(r => r.Reactions)
                   .WithOne(rr => rr.Reply)
                   .HasForeignKey(rr => rr.ReplyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
