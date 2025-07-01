namespace Domain.Entities
{
    public class Message : AuditableEntity
    {
        public string? Text { get; set; }

        public required Guid ChatId { get; set; }
        public Chat Chat { get; set; } = default!;

        public required Guid SenderId { get; set; }
        public MediaUser Sender { get; set; } = default!;

        public bool IsRead { get; set; } 

        public Guid? ReplyToMessageId { get; set; }
        public Message? ReplyToMessage { get; set; }

        public bool IsEdited { get; set; }
        public DateTime? EditedAt { get; set; }

        public ICollection<MessageMedia> MediaAttachments { get; set; } = [];
        public ICollection<MessageReaction> Reactions { get; set; } = [];

        //public string? SenderImageUrl { get; set; }
        //public required Guid ReciverId { get; set; }
        //public MediaUser Reciver { get; set; } = default!;
    }
}
