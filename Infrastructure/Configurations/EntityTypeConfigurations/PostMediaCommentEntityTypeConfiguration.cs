using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class PostMediaCommentEntityTypeConfiguration : IEntityTypeConfiguration<PostMediaComment>
    {
        public void Configure(EntityTypeBuilder<PostMediaComment> builder)
        {
            builder.ToTable("PostMediaComments");

            builder.HasKey(pmc => pmc.Id);

            builder.Property(pmc => pmc.Content).HasMaxLength(1500);
            builder.Property(pmc => pmc.MediaUrl).HasMaxLength(2048);

            builder.HasOne(pmc => pmc.MediaUser)
                   .WithMany(mu => mu.PostMediaComments)
                   .HasForeignKey(pmc => pmc.MediaUserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pmc => pmc.PostMedia)
                   .WithMany(pm => pm.Comments)
                   .HasForeignKey(pmc => pmc.PostMediaId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pmc => pmc.Reactions)
                   .WithOne(r => r.PostMediaComment)
                   .HasForeignKey(r => r.PostMediaCommentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pmc => pmc.Replies)
                   .WithOne(r => r.Comment)
                   .HasForeignKey(r => r.CommentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}