using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("posts");

            builder.HasKey(p =>  p.Id);
            //builder.Property(p => p.Title).IsRequired();
            //builder.Property(p => p.ContentType).IsRequired();
            builder.Property(p => p.ContentText);
            builder.Property(p => p.ContentUrl);
            builder.Property(p => p.PostVisibility).IsRequired();
            builder.Property(p => p.PosterId).IsRequired();

            builder.HasMany(p => p.PostComments)
               .WithOne(c => c.Post)
               .HasForeignKey(c => c.PostId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PostLikes)
                   .WithOne(l => l.Post)
                   .HasForeignKey(l => l.PostId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(p => p.IsDeleted);
        }
    }
}