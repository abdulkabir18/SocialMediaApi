using Domain.Enums;

namespace Domain.Entities
{
    public class PostMedia : AuditableEntity
    {
        public required Guid PostId { get; set; }
        public required string Url { get; set; } = default!;
        public required MediaType MediaType { get; set; }

        public Post Post { get; set; } = default!;
        public ICollection<PostMediaReaction> Reactions { get; set; } = [];
        public ICollection<PostMediaComment> Comments { get; set; } = [];
    }
}