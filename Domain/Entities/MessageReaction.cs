using Domain.Enums;

namespace Domain.Entities
{
    public class MessageReaction : AuditableEntity
    {
        public required ReactionType ReactionType { get; set; }

        public required Guid MessageId { get; set; }
        public Message Message { get; set; } = default!;

        public required Guid ReactorId { get; set; }
        public MediaUser Reactor { get; set; } = default!;
    }
}