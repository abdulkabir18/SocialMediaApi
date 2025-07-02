using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class MediaUserEntityTypeConfiguration : IEntityTypeConfiguration<MediaUser>
    {
        public void Configure(EntityTypeBuilder<MediaUser> builder)
        {
            builder.ToTable("Mediausers");

            builder.HasKey(a => a.Id);

            builder.HasIndex(a => a.UserName).IsUnique();
            builder.HasIndex(a => a.Email).IsUnique();
            builder.HasIndex(a => a.PhoneNumber).IsUnique();

            builder.Property(a => a.Email).IsRequired().HasMaxLength(255);
            builder.Property(a => a.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.LastName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Gender).IsRequired().HasMaxLength(8);
            builder.Property(a => a.DateOfBirth).HasConversion(v => v.ToDateTime(TimeOnly.MinValue), v => DateOnly.FromDateTime(v)).HasColumnType("date").IsRequired();
            builder.Property(a => a.CreatedBy).IsRequired();

            builder.Property(a => a.Address).HasMaxLength(255);
            builder.Property(a => a.ModifiedBy).HasMaxLength(100);
            builder.Property(a => a.UserName).HasMaxLength(50).IsRequired(false);
            builder.Property(a => a.Bio).HasMaxLength(500).IsRequired(false);
            builder.Property(a => a.ProfilePictureUrl).HasMaxLength(300).IsRequired(false);

            builder.Property(a => a.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(a => a.IsDeleted).HasDefaultValue(false);

            builder.HasMany(a => a.Posts)
                 .WithOne(p => p.Poster)
                 .HasForeignKey(p => p.PosterId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.PostMedias)
                .WithOne(p => p.MediaUser)
                .HasForeignKey(p => p.MediaUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.Comments)
                .WithOne(c => c.Commenter)
                .HasForeignKey(c => c.CommenterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.PostReactions)
                .WithOne(r => r.Reactor)
                .HasForeignKey(r => r.ReactorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.CommentReactions)
                .WithOne(r => r.Reactor)
                .HasForeignKey(r => r.ReactorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.ReplyReactions)
                .WithOne(r => r.Reactor)
                .HasForeignKey(r => r.ReactorId)
                .OnDelete (DeleteBehavior.Restrict);

            builder.HasMany(a => a.PostMediaReactions)
                .WithOne(r => r.Reactor)
                .HasForeignKey(r => r.ReactorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.PostMediaComments)
                .WithOne(c => c.MediaUser)
                .HasForeignKey (c => c.MediaUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.PostMediaCommentReactions)
                .WithOne(r => r.Reactor)
                .HasForeignKey(r => r.ReactorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.PostMediaCommentReplies)
                .WithOne(r => r.Replyer)
                .HasForeignKey(r => r.ReplyerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.PostMediaCommentReplyReactions)
                .WithOne(r => r.Reactor)
                .HasForeignKey(r => r.ReactorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.MessageReactions)
                .WithOne(r => r.Reactor)
                .HasForeignKey(r => r.ReactorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.FriendsAccepted)
                .WithOne(f => f.Addressee)
                .HasForeignKey(f => f.AddresseeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.FriendsRequested)
                .WithOne(f => f.Requester)
                .HasForeignKey(f => f.AddresseeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.ChatMediaUsers)
                .WithOne(c => c.MediaUser)
                .HasForeignKey(c => c.MediaUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.Messages)
                .WithOne(m => m.Sender)
                .HasForeignKey(s => s.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.Notifications)
                .WithOne(n => n.MediaUser)
                .HasForeignKey(m => m.MediaUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}