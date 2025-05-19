using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class FriendEntityTypeConfiguration : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.RequesterId).IsRequired();
            builder.Property(f => f.AddresseeId).IsRequired();
            builder.Property(f => f.Status).IsRequired();

            builder.HasOne(f => f.Requester)
               .WithMany(m => m.RequstedFriends)
               .HasForeignKey(f => f.RequesterId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Addressee)
                   .WithMany(m => m.AcceptedFriends)
                   .HasForeignKey(f => f.AddresseeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(f => f.CreatedBy);
            builder.Ignore(f => f.ModifiedBy);
        }
    }
}
