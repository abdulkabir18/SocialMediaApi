using Domain.Enums;

namespace Domain.Entities
{
    public class PostMediaCommentReaction : AuditableEntity
    {
        public required ReactionType ReactionType { get; set; }

        public required Guid PostMediaCommentId { get; set; }
        public required Guid ReactorId { get; set; }

        public PostMediaComment PostMediaComment { get; set; } = default!;
        public MediaUser Reactor { get; set; } = default!;
    }
}