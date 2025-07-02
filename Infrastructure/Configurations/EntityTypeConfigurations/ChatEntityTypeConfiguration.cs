using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class ChatEntityTypeConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable("Chats");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.ChatImageUrl).IsRequired(false).HasMaxLength(250);
            builder.Property(c => c.ChatName).IsRequired().HasMaxLength(500);
            builder.Property(c => c.IsGroup).HasDefaultValue(false);

            builder.HasMany(c => c.ChatMessages)
               .WithOne(m => m.Chat)
               .HasForeignKey(m => m.ChatId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.ChatMediaUsers)
                .WithOne(c => c.Chat)
                .HasForeignKey(c => c.ChatId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}