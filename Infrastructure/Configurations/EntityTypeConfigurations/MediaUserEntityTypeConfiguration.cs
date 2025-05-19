using System.Reflection.Emit;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class MediaUserEntityTypeConfiguration : IEntityTypeConfiguration<MediaUser>
    {
        public void Configure(EntityTypeBuilder<MediaUser> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.UserName);
            builder.HasIndex(u => u.Email);
            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.LastName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Address).IsRequired().HasMaxLength(200);
            builder.Property(u => u.Gender).IsRequired().HasMaxLength(8);
            builder.Property(a => a.DateOfBirth).HasConversion(v => v.ToDateTime(TimeOnly.MinValue), v => DateOnly.FromDateTime(v)).IsRequired();

            builder.HasMany(m => m.Posts)
               .WithOne(p => p.Poster)
               .HasForeignKey(p => p.PosterId);

            builder.HasMany(m => m.Comments)
                   .WithOne(c => c.Commenter)
                   .HasForeignKey(c => c.CommenterId);

            builder.HasMany(m => m.Replies)
                   .WithOne(r => r.Replyer)
                   .HasForeignKey(r => r.ReplyerId);

            builder.HasMany(m => m.Likes)
                   .WithOne(l => l.Liker)
                   .HasForeignKey(l => l.LikerId);

            builder.HasMany(m => m.Chats)
                   .WithOne(c => c.MediaUser)
                   .HasForeignKey(c => c.MediaUserId);

            builder.HasMany(m => m.RequstedFriends)
                   .WithOne(f => f.Requester)
                   .HasForeignKey(f => f.RequesterId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(m => m.AcceptedFriends)
                   .WithOne(f => f.Addressee)
                   .HasForeignKey(f => f.AddresseeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
