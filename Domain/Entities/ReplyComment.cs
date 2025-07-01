using Domain.Enums;

namespace Domain.Entities
{
    public class ReplyComment : AuditableEntity
    {
        public string? Content { get; set; }
        public string? MediaUrl { get; set; }
        public MediaType? MediaType { get; set; }
        public required Guid ReplyerId { get; set; }

        // this is reply to comment
        public Guid? CommentId { get; set; }

        // this is reply to another reply
        public Guid? ParentReplyId { get; set; }
        public ReplyComment? ParentReply { get; set; }

        public MediaUser Replyer { get; set; } = default!;
        public PostComment Comment { get; set; } = default!;

        public ICollection<ReplyReaction> Reactions { get; set; } = [];
        public ICollection<ReplyComment> Replies { get; set; } = [];
    }
}