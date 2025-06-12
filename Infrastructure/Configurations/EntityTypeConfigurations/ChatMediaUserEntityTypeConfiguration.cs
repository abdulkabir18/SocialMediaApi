using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class ChatMediaUserEntityTypeConfiguration : IEntityTypeConfiguration<ChatMediaUser>
    {
        public void Configure(EntityTypeBuilder<ChatMediaUser> builder)
        {
            builder.ToTable("chatmediaUsers");

            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Chat)
                .WithMany(c => c.ChatMediaUsers)
                .HasForeignKey(c => c.ChatId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(c => c.MediaUser)
                .WithMany(c => c.ChatMediaUsers)
                .HasForeignKey(c => c.ChatId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}