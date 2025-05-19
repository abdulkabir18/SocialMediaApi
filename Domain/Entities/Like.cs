namespace Domain.Entities
{
    public class Like : AuditableEntity
    {
        public Guid? PostId { get; set; }
        public Post Post { get; set; } = default!;
        public Guid? CommentId { get; set; }
        public Comment Comment { get; set; } = default!;
        public Guid? ReplyId { get; set; }
        public Reply Reply { get; set; } = default!;
        public required Guid LikerId { get; set; }
        public MediaUser Liker { get; set; } = default!;

    }
}
