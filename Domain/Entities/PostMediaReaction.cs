using Domain.Enums;

namespace Domain.Entities
{
    public class PostMediaReaction : AuditableEntity
    {
        public required ReactionType ReactionType { get; set; }
        public required Guid ReactorId { get; set; }
        public required Guid PostMediaId { get; set; }

        public MediaUser Reactor { get; set; } = default!;
        public PostMedia PostMedia { get; set; } = default!;
    }
}