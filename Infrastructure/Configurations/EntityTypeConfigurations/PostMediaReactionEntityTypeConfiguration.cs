using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class PostMediaReactionEntityTypeConfiguration : IEntityTypeConfiguration<PostMediaReaction>
    {
        public void Configure(EntityTypeBuilder<PostMediaReaction> builder)
        {
            builder.ToTable("PostMediaReactions");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.ReactionType).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ModifiedBy).HasMaxLength(100);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.HasOne(x => x.Reactor)
                   .WithMany(x => x.PostMediaReactions)
                   .HasForeignKey(x => x.ReactorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.PostMedia)
                   .WithMany(x => x.Reactions)
                   .HasForeignKey(x => x.PostMediaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}