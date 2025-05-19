using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(m => m.Text).IsRequired();
            builder.Property(m => m.ChatId).IsRequired();
            builder.Property(m => m.SenderId).IsRequired();
            builder.Property(m => m.ReciverId).IsRequired();

            builder.HasOne(c => c.Chat)
               .WithMany(c => c.Messages)
               .HasForeignKey(m => m.ChatId);
        }
    }
}
