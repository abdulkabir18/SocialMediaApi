namespace Domain.Entities
{
    public class Reply : AuditableEntity
    {
        public required string Text { get; set; }
        public required Guid ReplyerId { get; set; }
        public MediaUser Replyer { get; set; } = default!;
        public required Guid CommentId { get; set; }
        public Comment Comment { get; set; } = default!;
        public ICollection<Reply> ReplyReplies { get; set; } = [];
        public ICollection<Like> ReplyLikes { get; set; } = [];
    }
}
