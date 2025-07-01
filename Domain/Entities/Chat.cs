namespace Domain.Entities
{
    public class Chat : AuditableEntity
    {
        public required string ChatName { get; set; }
        public string? ChatImageUrl { get; set; }

        public bool IsGroup { get; set; }

        public ICollection<Message> ChatMessages { get; set; } = [];
        public ICollection<ChatMediaUser> ChatMediaUsers { get; set; } = [];

        //public required Guid MediaUserId { get; set; }
        //public MediaUser MediaUser { get; set; } = default!;
    }
}
