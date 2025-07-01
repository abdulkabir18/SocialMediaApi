using Domain.Enums;

namespace Domain.Entities
{
    public class CommentReaction : AuditableEntity
    {
        public required ReactionType ReactionType { get; set; }
        public required Guid ReactorId { get; set; }
        public required Guid CommentId { get; set; }

        public MediaUser Reactor { get; set; } = default!;
        public PostComment Comment { get; set; } = default!;
    }

    //public class Comment : AuditableEntity
    //{
    //    public required string Text { get; set; }
    //    public required Guid PostId { get; set; }
    //    public Post Post { get; set; } = default!;
    //    public required Guid CommenterId { get; set; }
    //    public MediaUser Commenter { get; set; } = default!;
    //    public ICollection<Reply> CommentReplies { get; set; } = [];
    //    public ICollection<Like> CommentLikes { get; set; } = [];
    //}
}
