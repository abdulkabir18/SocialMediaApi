using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class MessageMediaEntityTypeConfiguration : IEntityTypeConfiguration<MessageMedia>
    {
        public void Configure(EntityTypeBuilder<MessageMedia> builder)
        {
            builder.ToTable("MessageMedias");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.MediaUrl).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.MediaType).HasConversion<int>().IsRequired();

            builder.HasOne(x => x.Message)
                   .WithMany(x => x.MediaAttachments)
                   .HasForeignKey(x => x.MessageId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}