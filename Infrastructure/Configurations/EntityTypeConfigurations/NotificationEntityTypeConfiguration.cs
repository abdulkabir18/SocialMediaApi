using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class NotificationEntityTypeConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Message).IsRequired().HasMaxLength(500);

            builder.Property(n => n.IsRead)
                   .HasDefaultValue(false);

            builder.Property(n => n.ReadAt)
                   .IsRequired(false);

            builder.Property(n => n.Type).HasConversion<int>().HasDefaultValue(NotificationType.Generic);

            builder.Property(n => n.SourceEntityId)
                   .IsRequired(false);

            builder.HasOne(n => n.MediaUser)
                   .WithMany(u => u.Notifications)
                   .HasForeignKey(n => n.MediaUserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}