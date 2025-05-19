namespace Domain.Entities
{
    public class Comment : AuditableEntity
    {
        public required string Text { get; set; }
        public required Guid PostId { get; set; }
        public Post Post { get; set; } = default!;
        public required Guid CommenterId { get; set; }
        public MediaUser Commenter { get; set; } = default!;
        public ICollection<Reply> Replies { get; set; } = [];
        public ICollection<Like> Likes { get; set; } = [];
    }
}
