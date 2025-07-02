using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class MessageReactionEntityTypeConfiguration : IEntityTypeConfiguration<MessageReaction>
    {
        public void Configure(EntityTypeBuilder<MessageReaction> builder)
        {
            builder.ToTable("MessageReactions");

            builder.HasKey(mr => mr.Id);
            builder.Property(mr => mr.ReactionType).IsRequired().HasConversion<int>();

            builder.HasOne(mr => mr.Message)
                   .WithMany(m => m.Reactions)
                   .HasForeignKey(mr => mr.MessageId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(mr => mr.Reactor)
                   .WithMany(u => u.MessageReactions)
                   .HasForeignKey(mr => mr.ReactorId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}