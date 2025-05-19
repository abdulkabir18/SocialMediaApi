namespace Domain.Entities
{
    public class Message : AuditableEntity
    {
        public required string Text { get; set; }
        public required Guid ChatId { get; set; }
        public Chat Chat { get; set; } = default!;
        public required Guid SenderId { get; set; }
        public MediaUser Sender { get; set; } = default!;
        public required Guid ReciverId { get; set; }
        public MediaUser Reciver { get; set; } = default!;
    }
}
