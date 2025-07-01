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
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostMedia> PostMedias { get; set; }
        public DbSet<PostMediaComment> PostMediaComments { get; set; }
        public DbSet<PostMediaReaction> PostMediaReactions { get; set; }
        public DbSet<PostMediaCommentReaction> PostMediaCommentReactions { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<ReplyComment> ReplyComments { get; set; }
        public DbSet<PostReaction> PostReactions { get; set; }
        public DbSet<CommentReaction> CommentReactions { get; set; }
        public DbSet<ReplyReaction> ReplyReactions { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMediaUser> ChatMediaUsers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageMedia> MessageMedias { get; set; }
        public DbSet<MessageReaction> MessageReactions { get; set; }
        public DbSet<Notification> Notifications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
