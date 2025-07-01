using Domain.Enums;

namespace Domain.Entities
{
    public class Post : AuditableEntity
    {
        //public required string Title { get; set; }
        //public required ContentType ContentType { get; set; }
        //public string? ContentUrl { get; set; }


        public required Guid PosterId { get; set; }
        public string? ContentText { get; set; }
        public required PostVisibility PostVisibility { get; set; }
        public required PostType Type { get; set; }
        public string? BackgroundStyleId { get; set; }


        public MediaUser Poster { get; set; } = default!;
        public ICollection<PostMedia> Medias { get; set; } = [];
        public ICollection<PostComment> PostComments { get; set; } = [];
        public ICollection<PostReaction> PostReactions { get; set; } = [];
    }
}
