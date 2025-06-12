namespace Domain.Entities
{
    public class MediaUser : AuditableEntity
    {
        public string? ProfilePictureUrl { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public string? UserName { get; set; }
        public string? Address { get; set; }
        public required string Gender { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public string? Bio {  get; set; }
        public ICollection<ChatMediaUser> ChatMediaUsers { get; set; } = [];
        public ICollection<Post> MediaUserPosts { get; set; } = [];
        public ICollection<Comment> MediaUserComments { get; set; } = [];
        public ICollection<Reply> MediaUserReplies { get; set; } = [];
        public ICollection<Like> MediaUserLikes { get; set; } = [];
        public ICollection<Friend> MediaUserAcceptedFriends { get; set; } = [];
        public ICollection<Friend> MediaUserRequstedFriends { get; set; } = [];
    }
}