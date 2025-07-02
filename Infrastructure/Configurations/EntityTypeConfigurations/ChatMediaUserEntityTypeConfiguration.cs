using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class ChatMediaUserEntityTypeConfiguration : IEntityTypeConfiguration<ChatMediaUser>
    {
        public void Configure(EntityTypeBuilder<ChatMediaUser> builder)
        {
            builder.ToTable("ChatMediaUsers");

            builder.HasKey(cmu => cmu.Id);
            builder.Property(cmu => cmu.IsAdmin).IsRequired();
            builder.Property(cmu => cmu.IsMuted).IsRequired();

            builder.HasOne(cmu => cmu.Chat)
                   .WithMany(c => c.ChatMediaUsers)
                   .HasForeignKey(cmu => cmu.ChatId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cmu => cmu.MediaUser)
                   .WithMany(mu => mu.ChatMediaUsers)
                   .HasForeignKey(cmu => cmu.MediaUserId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}