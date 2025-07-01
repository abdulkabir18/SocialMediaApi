using Domain.Enums;

namespace Domain.Entities
{
    public class ReplyReaction : AuditableEntity
    {
        public required ReactionType ReactionType { get; set; }
        public required Guid ReplyCommentId { get; set; }
        public required Guid ReactorId { get; set; }

        public MediaUser Reactor { get; set; } = default!;
        public ReplyComment ReplyComment { get; set; } = default!;
    }
}