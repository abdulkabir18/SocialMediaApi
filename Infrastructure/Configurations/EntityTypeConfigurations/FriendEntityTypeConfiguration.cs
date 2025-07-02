using  Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class FriendEntityTypeConfiguration : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.ToTable("Friends");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Status).IsRequired().HasConversion<int>();

            builder.HasOne(f => f.Requester)
                   .WithMany(u => u.FriendsRequested)
                   .HasForeignKey(f => f.RequesterId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Addressee)
                   .WithMany(u => u.FriendsAccepted)
                   .HasForeignKey(f => f.AddresseeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
