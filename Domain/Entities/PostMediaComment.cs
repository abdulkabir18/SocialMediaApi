using Domain.Enums;

namespace Domain.Entities
{
    public class PostMediaComment : AuditableEntity
    {
        public string? Content { get; set; }
        public string? MediaUrl { get; set; }
        public MediaType? MediaType { get; set; }

        public required Guid PostMediaId { get; set; }
        public required Guid MediaUserId { get; set; }

        public MediaUser MediaUser { get; set; } = default!;
        public PostMedia PostMedia { get; set; } = default!;

        public ICollection<PostMediaCommentReaction> Reactions { get; set; } = [];
        public ICollection<PostMediaCommentReply> Replies { get; set; } = [];
    }
}
