using Domain.Enums;

namespace Domain.Entities
{
    public class PostMediaCommentReply : AuditableEntity
    {
        public string? Content { get; set; }
        public string? MediaUrl { get; set; }
        public MediaType? MediaType { get; set; }

        public required Guid ReplyerId { get; set; }

        // this is for reply to comment
        public Guid? CommentId { get; set; }

        // this is for reply to another reply
        public Guid? ParentReplyId { get; set; }


        public MediaUser Replyer { get; set; } = default!;
        public PostMediaComment? Comment { get; set; }
        public PostMediaCommentReply? ParentReply { get; set; }

        public ICollection<PostMediaCommentReply> Replies { get; set; } = [];
        public ICollection<PostMediaCommentReplyReaction> Reactions { get; set; } = [];
    }
}
