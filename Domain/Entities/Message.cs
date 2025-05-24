namespace Domain.Entities
{
    public class Message : AuditableEntity
    {
        public string? Text { get; set; }
        public string? AttachmentUrl { get; set; }
        public required Guid ChatId { get; set; }
        public Chat Chat { get; set; } = default!;
        public required Guid SenderId { get; set; }
        public MediaUser Sender { get; set; } = default!;
        public string? SenderImageUrl { get; set; }
        public bool IsRead { get; set; } 
        //public required Guid ReciverId { get; set; }
        //public MediaUser Reciver { get; set; } = default!;
    }
}
