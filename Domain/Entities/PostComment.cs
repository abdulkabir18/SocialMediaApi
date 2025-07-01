using Domain.Enums;

namespace Domain.Entities
{
    public class PostComment : AuditableEntity
    {
        public string? Content { get; set; }
        public string? MediaUrl { get; set; }
        public MediaType? MediaType { get; set; }
        public required Guid PostId { get; set; }
        public required Guid CommenterId { get; set; }

        public MediaUser Commenter { get; set; } = default!;
        public Post Post { get; set; } = default!;
        public List<CommentReaction> Reactions { get; set; } = [];
        public List<ReplyComment> Replies { get; set; } = [];
    }

    //public class Comment : AuditableEntity
    //{
    //    public required string Text { get; set; }
    //    public required Guid PostId { get; set; }
    //    public Post Post { get; set; } = default!;
    //    public required Guid CommenterId { get; set; }
    //    public MediaUser Commenter { get; set; } = default!;
    //    public ICollection<Reply> CommentReplies { get; set; } = [];
    //    public ICollection<Like> CommentLikes { get; set; } = [];
    //}
}
