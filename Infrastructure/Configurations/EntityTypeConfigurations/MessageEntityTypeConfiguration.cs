using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Text).HasMaxLength(1000);
            builder.Property(m => m.IsRead).IsRequired();
            builder.Property(m => m.IsEdited).HasDefaultValue(false);
            builder.Property(m => m.EditedAt).IsRequired(false);

            builder.HasOne(m => m.Chat)
                   .WithMany(c => c.ChatMessages)
                   .HasForeignKey(m => m.ChatId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Sender)
                   .WithMany(u => u.Messages)
                   .HasForeignKey(m => m.SenderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.ReplyToMessage)
                   .WithMany()
                   .HasForeignKey(m => m.ReplyToMessageId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(m => m.MediaAttachments)
                   .WithOne(ma => ma.Message)
                   .HasForeignKey(ma => ma.MessageId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.Reactions)
                   .WithOne(r => r.Message)
                   .HasForeignKey(r => r.MessageId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
