using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");

            builder.HasKey(p =>  p.Id);
            builder.Property(p => p.ContentText).HasMaxLength(10000);
            builder.Property(p => p.Type).IsRequired();
            builder.Property(p => p.PostVisibility).IsRequired();
            builder.Property(p => p.PosterId).IsRequired();
            builder.Property(p => p.BackgroundStyleId).HasMaxLength(50); 
            builder.Property(p => p.CreatedBy).IsRequired();
            builder.Property(p => p.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.HasIndex(p => p.PosterId);
            builder.HasIndex(p => p.Type);

            builder.HasOne(p => p.Poster)
                   .WithMany(mu => mu.Posts)
                   .HasForeignKey(p => p.PosterId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Medias)
                   .WithOne(pm => pm.Post)
                   .HasForeignKey(pm => pm.PostId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PostComments)
                   .WithOne(c => c.Post)
                   .HasForeignKey(c => c.PostId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PostReactions)
                   .WithOne(r => r.Post)
                   .HasForeignKey(r => r.PostId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
