using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class PostReactionEntityTypeConfiguration : IEntityTypeConfiguration<PostReaction>
    {
        public void Configure(EntityTypeBuilder<PostReaction> builder)
        {
            builder.ToTable("PostReactions");
            builder.HasKey(pr => pr.Id);
            builder.HasIndex(pr => new { pr.ReactorId, pr.PostId }).IsUnique();
            builder.Property(pr => pr.ReactionType).HasConversion<int>().IsRequired();
            builder.Property(pr => pr.CreatedBy).IsRequired();
            builder.Property(pr => pr.IsDeleted).HasDefaultValue(false);

            builder.HasOne(pr => pr.Reactor)
                   .WithMany(mu => mu.PostReactions)
                   .HasForeignKey(pr => pr.ReactorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pr => pr.Post)
                   .WithMany(p => p.PostReactions)
                   .HasForeignKey(pr => pr.PostId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
