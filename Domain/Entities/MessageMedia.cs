using Domain.Enums;

namespace Domain.Entities
{
    public class MessageMedia : AuditableEntity
    {
        public required Guid MessageId { get; set; }
        public Message Message { get; set; } = default!;

        public required string MediaUrl { get; set; }
        public MediaType MediaType { get; set; }
    }
}