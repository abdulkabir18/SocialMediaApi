using Domain.Enums;

namespace Domain.Entities
{
    public class PostReaction : AuditableEntity
    {
        public required ReactionType ReactionType { get; set; }
        public required Guid ReactorId { get; set; }
        public required Guid PostId { get; set; }

        public MediaUser Reactor { get; set; } = default!;
        public Post Post { get; set; } = default!;
    }

    //public class Like : AuditableEntity
    //{
    //    public Guid? PostId { get; set; }
    //    public Post Post { get; set; } = default!;
    //    public Guid? CommentId { get; set; }
    //    public Comment Comment { get; set; } = default!;
    //    public Guid? ReplyId { get; set; }
    //    public Reply Reply { get; set; } = default!;
    //    public required Guid LikerId { get; set; }
    //    public MediaUser Liker { get; set; } = default!;

    //}
}
