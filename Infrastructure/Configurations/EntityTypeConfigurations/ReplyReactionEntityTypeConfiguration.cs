using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class ReplyReactionEntityTypeConfiguration : IEntityTypeConfiguration<ReplyReaction>
    {
        public void Configure(EntityTypeBuilder<ReplyReaction> builder)
        {
            builder.ToTable("ReplyReactions");
            builder.HasKey(rr => rr.Id);
            builder.Property(rr => rr.ReactionType).HasConversion<int>().IsRequired();
            builder.Property(rr => rr.CreatedBy).IsRequired();
            builder.Property(rr => rr.IsDeleted).HasDefaultValue(false);
            builder.HasIndex(rr => new { rr.ReactorId, rr.ReplyCommentId }).IsUnique();

            builder.HasOne(rr => rr.Reactor)
                   .WithMany(mu => mu.ReplyReactions)
                   .HasForeignKey(rr => rr.ReactorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(rr => rr.ReplyComment)
                   .WithMany(rc => rc.Reactions)
                   .HasForeignKey(rr => rr.ReplyCommentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
