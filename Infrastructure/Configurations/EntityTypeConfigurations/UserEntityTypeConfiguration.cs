using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Salt).IsRequired();
            builder.HasIndex(u => u.PhoneNumber).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
            builder.Property(u => u.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(u => u.Password).IsRequired();
            builder.Property(u => u.LoggedInDeviceIpAddress).HasMaxLength(100).IsRequired(false);
            builder.Property(u => u.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(u => u.IsDeleted).HasDefaultValue(false);
            builder.Property(u => u.IsActive).HasDefaultValue(true);
            builder.Property(u => u.IsEmailVerified).HasDefaultValue(false);
            builder.Property(u => u.IsPhoneNumberVerified).HasDefaultValue(false);
            builder.Property(u => u.IsOnline).HasDefaultValue(false);
            builder.Property(u => u.LastSeen).IsRequired(false);
            builder.Property(u => u.DeactivatedAt).IsRequired(false);
            builder.Property(u => u.CreatedBy).IsRequired();
            builder.Property(u => u.ModifiedBy).IsRequired(false);
        }
    }
}
