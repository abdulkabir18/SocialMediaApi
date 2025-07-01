using Domain.Enums;

namespace Domain.Entities
{
    public class PostMediaCommentReplyReaction : AuditableEntity
    {
        public required ReactionType ReactionType { get; set; }
        public required Guid ReplyId { get; set; }
        public required Guid ReactorId { get; set; }

        public MediaUser Reactor { get; set; } = default!;
        public PostMediaCommentReply Reply { get; set; } = default!;
    }
}