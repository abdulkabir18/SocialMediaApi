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

            builder.HasKey(x => x.Id);
            builder.Property(m => m.SenderImageUrl).IsRequired(false);
            builder.Property(m => m.Text).IsRequired(false);
            builder.Property(m => m.AttachmentUrl).IsRequired(false);
            builder.Property(m => m.ChatId).IsRequired();
            builder.Property(m => m.SenderId).IsRequired();
            //builder.Property(m => m.ReciverId).IsRequired();

            builder.HasOne(c => c.Chat)
               .WithMany(c => c.ChatMessages)
               .HasForeignKey(m => m.ChatId)
               .IsRequired();
        }
    }
}