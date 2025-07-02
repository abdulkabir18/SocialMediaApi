using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class CommentReactionEntityTypeConfiguration : IEntityTypeConfiguration<CommentReaction>
    {
        public void Configure(EntityTypeBuilder<CommentReaction> builder)
        {
            builder.ToTable("CommentReactions");
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.ReactionType).HasConversion<int>().IsRequired();
            builder.Property(cr => cr.CreatedBy).IsRequired();
            builder.Property(cr => cr.IsDeleted).HasDefaultValue(false);
            builder.HasIndex(cr => new { cr.ReactorId, cr.CommentId }).IsUnique();

            builder.HasOne(cr => cr.Reactor)
                   .WithMany(mu => mu.CommentReactions)
                   .HasForeignKey(cr => cr.ReactorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cr => cr.Comment)
                   .WithMany(pc => pc.Reactions)
                   .HasForeignKey(cr => cr.CommentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}