using Domain.Enums;

namespace Domain.Entities
{
    public class Post : AuditableEntity
    {
        //public required string Title { get; set; }
        //public required ContentType ContentType { get; set; }
        public string? ContentText { get; set; }
        public string? ContentUrl { get; set; }
        public required PostVisibility PostVisibility { get; set; } = PostVisibility.Friends;
        public required Guid PosterId { get; set; }
        public MediaUser Poster { get; set; } = default!;
        public ICollection<Comment> PostComments { get; set; } = [];
        public ICollection<Like> PostLikes { get; set; } = [];
    }
}
