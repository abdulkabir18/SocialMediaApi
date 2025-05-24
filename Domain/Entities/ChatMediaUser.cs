namespace Domain.Entities
{
    public class ChatMediaUser : AuditableEntity
    {
        public required Guid ChatId { get; set; }
        public required Chat Chat { get; set; }
        public required Guid MediaUserId { get; set; }
        public required MediaUser MediaUser { get; set; }
    }
}