using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class PostMediaCommentReplyReactionConfiguration : IEntityTypeConfiguration<PostMediaCommentReplyReaction>
    {
        public void Configure(EntityTypeBuilder<PostMediaCommentReplyReaction> builder)
        {
            builder.ToTable("Postmediacommentreplyreactions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ReactionType).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ModifiedBy).HasMaxLength(100);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.HasOne(x => x.Reactor)
                   .WithMany(u => u.PostMediaCommentReplyReactions)
                   .HasForeignKey(x => x.ReactorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Reply)
                   .WithMany(r => r.Reactions)
                   .HasForeignKey(x => x.ReplyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
