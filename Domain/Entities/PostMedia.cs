using Domain.Enums;

namespace Domain.Entities
{
    public class PostMedia : AuditableEntity
    {
        public required MediaType MediaType { get; set; }
        public required string Url { get; set; } 
        public required Guid PostId { get; set; }
        public required Guid MediaUserId { get; set; }

        public MediaUser MediaUser { get; set; } = default!;
        public Post Post { get; set; } = default!;
        public ICollection<PostMediaReaction> Reactions { get; set; } = [];
        public ICollection<PostMediaComment> Comments { get; set; } = [];
    }
}