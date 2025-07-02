using Domain.Enums;

namespace Domain.Entities
{
    public class Notification : AuditableEntity
    {
        public Guid MediaUserId { get; set; }
        public MediaUser MediaUser { get; set; } = default!;
        public string Message { get; set; } = default!;
        public bool IsRead { get; set; } = false;
        public DateTime? ReadAt { get; set; }
        public NotificationType Type { get; set; } = NotificationType.Generic;
        public Guid? SourceEntityId { get; set; }
    }
}
