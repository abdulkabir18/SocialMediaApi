using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class PostMediaCommentReactionEntityTypeConfiguration : IEntityTypeConfiguration<PostMediaCommentReaction>
    {
        public void Configure(EntityTypeBuilder<PostMediaCommentReaction> builder)
        {
            builder.ToTable("PostMediaCommentReactions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ReactionType).HasConversion<int>().IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.HasIndex(x => new { x.ReactorId, x.PostMediaCommentId }).IsUnique();

            builder.HasOne(x => x.PostMediaComment)
                   .WithMany(x => x.Reactions)
                   .HasForeignKey(x => x.PostMediaCommentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Reactor)
                   .WithMany(x => x.PostMediaCommentReactions)
                   .HasForeignKey(x => x.ReactorId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}