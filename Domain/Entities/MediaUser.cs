namespace Domain.Entities
{
    public class MediaUser : AuditableEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? UserName { get; set; }
        public string? Address { get; set; }
        public required string Gender { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public ICollection<Chat> Chats { get; set; } = [];
        public ICollection<Post> Posts { get; set; } = [];
        public ICollection<Comment> Comments { get; set; } = [];
        public ICollection<Reply> Replies { get; set; } = [];
        public ICollection<Like> Likes { get; set; } = [];
        public ICollection<Friend> AcceptedFriends { get; set; } = [];
        public ICollection<Friend> RequstedFriends { get; set; } = [];
    }
}
