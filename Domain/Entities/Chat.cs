namespace Domain.Entities
{
    public class Chat : AuditableEntity
    {
        public required Guid MediaUserId { get; set; }
        public MediaUser MediaUser { get; set; } = default!;
        public ICollection<Message> Messages { get; set; } = [];
    }
}
