using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntityTypeConfigurations
{
    public class ReplyEntityTypeConfiguration : IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> builder)
        {
            builder.ToTable("replies");

            builder.HasKey(r =>  r.Id);
            builder.Property(r => r.Text).IsRequired();
            builder.Property(r => r.ReplyerId).IsRequired();
            builder.Property(r => r.CommentId).IsRequired();

            builder.HasMany(r => r.ReplyLikes)
               .WithOne(l => l.Reply)
               .HasForeignKey(l => l.ReplyId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(r => r.IsDeleted);
        }
    }
}
