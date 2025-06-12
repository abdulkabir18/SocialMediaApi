using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistense.Context
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }   
        public DbSet<MediaUser> MediaUsers { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Post> Posts { get;set; }
        public DbSet<Comment> Comments{ get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Reply> Replys { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMediaUser> ChatMediaUsers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
