using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class ChatEntityTypeConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable("chats");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.ChatImageUrl).IsRequired(false);
            builder.Property(c => c.ChatName).IsRequired().HasMaxLength(500);

            builder.HasMany(c => c.ChatMessages)
               .WithOne(m => m.Chat)
               .HasForeignKey(m => m.ChatId);

            builder.HasMany(c => c.ChatMediaUsers)
                .WithOne(c => c.Chat)
                .HasForeignKey(c => c.ChatId);

            builder.Ignore(c => c.CreatedBy);
            builder.Ignore(c => c.ModifiedBy);
        }
    }
}