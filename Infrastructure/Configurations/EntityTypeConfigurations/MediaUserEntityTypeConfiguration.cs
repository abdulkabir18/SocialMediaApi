using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class MediaUserEntityTypeConfiguration : IEntityTypeConfiguration<MediaUser>
    {
        public void Configure(EntityTypeBuilder<MediaUser> builder)
        {
            builder.ToTable("mediausers");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.UserName).HasMaxLength(50).IsRequired(false);
            builder.Property(a => a.Bio).HasMaxLength(500).IsRequired(false);
            builder.Property(a => a.ProfilePictureUrl).HasMaxLength(300).IsRequired(false);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(a => a.PhoneNumber);
            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.LastName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Address).IsRequired().HasMaxLength(200);
            builder.Property(u => u.Gender).IsRequired().HasMaxLength(8);
            builder.Property(a => a.DateOfBirth).HasConversion(v => v.ToDateTime(TimeOnly.MinValue), v => DateOnly.FromDateTime(v)).HasColumnType("date").IsRequired();

            builder.HasMany(m => m.MediaUserPosts)
               .WithOne(p => p.Poster)
               .HasForeignKey(p => p.PosterId)
               .IsRequired();

            builder.HasMany(m => m.MediaUserComments)
                   .WithOne(c => c.Commenter)
                   .HasForeignKey(c => c.CommenterId)
                   .IsRequired();

            builder.HasMany(m => m.MediaUserReplies)
                   .WithOne(r => r.Replyer)
                   .HasForeignKey(r => r.ReplyerId)
                   .IsRequired();

            builder.HasMany(m => m.MediaUserLikes)
                   .WithOne(l => l.Liker)
                   .HasForeignKey(l => l.LikerId)
                   .IsRequired();

            builder.HasMany(m => m.ChatMediaUsers)
                   .WithOne(c => c.MediaUser)
                   .HasForeignKey(c => c.MediaUserId)
                   .IsRequired();

            builder.HasMany(m => m.MediaUserRequstedFriends)
                   .WithOne(f => f.Requester)
                   .HasForeignKey(f => f.RequesterId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            builder.HasMany(m => m.MediaUserAcceptedFriends)
                   .WithOne(f => f.Addressee)
                   .HasForeignKey(f => f.AddresseeId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        }
    }
}