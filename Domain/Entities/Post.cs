using Domain.Enums;

namespace Domain.Entities
{
    public class Post : AuditableEntity
    {
        public required string Title { get; set; }
        public required ContentType ContentType { get; set; }
        public required string Content { get; set; }
        public required Guid PosterId { get; set; }
        public MediaUser Poster { get; set; } = default!;
        public ICollection<Comment> Comments { get; set; } = [];
        public ICollection<Like> Likes { get; set; } = [];
    }
}
