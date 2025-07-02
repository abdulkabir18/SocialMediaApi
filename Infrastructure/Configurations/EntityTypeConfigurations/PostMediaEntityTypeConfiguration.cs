using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class PostMediaEntityTypeConfiguration : IEntityTypeConfiguration<PostMedia>
    {
        public void Configure(EntityTypeBuilder<PostMedia> builder)
        {
            builder.ToTable("PostMedias");
            builder.HasKey(pm => pm.Id);

            builder.Property(pm => pm.Url).IsRequired().HasMaxLength(2048);
            builder.Property(pm => pm.MediaType).IsRequired();
            builder.Property(pm => pm.CreatedBy).IsRequired();
            builder.Property(pm => pm.IsDeleted).HasDefaultValue(false);

            builder.HasOne(pm => pm.MediaUser)
                   .WithMany(mu => mu.PostMedias)
                   .HasForeignKey(pm => pm.MediaUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pm => pm.Post)
                   .WithMany(p => p.Medias)
                   .HasForeignKey(pm => pm.PostId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pm => pm.Reactions)
                   .WithOne(r => r.PostMedia)
                   .HasForeignKey(r => r.PostMediaId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pm => pm.Comments)
                   .WithOne(c => c.PostMedia)
                   .HasForeignKey(c => c.PostMediaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}