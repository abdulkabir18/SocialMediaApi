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
        public string? Bio { get; set; }

        public ICollection<ChatMediaUser> ChatMediaUsers { get; set; } = [];
        public ICollection<MessageReaction> MessageReactions { get; set; } = [];

        public ICollection<Post> Posts { get; set; } = [];
        public ICollection<PostMedia> PostMedias { get; set; } = [];

        public ICollection<PostComment> Comments { get; set; } = [];
        public ICollection<ReplyComment> Replies { get; set; } = [];

        public ICollection<PostReaction> PostReactions { get; set; } = [];
        public ICollection<CommentReaction> CommentReactions { get; set; } = [];
        public ICollection<ReplyReaction> ReplyReactions { get; set; } = [];

        public ICollection<PostMediaReaction> PostMediaReactions { get; set; } = [];
        public ICollection<PostMediaComment> PostMediaComments { get; set; } = [];

        public ICollection<PostMediaCommentReply> PostMediaCommentReplies { get; set; } = [];
        public ICollection<PostMediaCommentReaction> PostMediaCommentReactions { get; set; } = [];
        public ICollection<PostMediaCommentReplyReaction> PostMediaCommentReplyReactions { get; set; } = [];


        public ICollection<Friend> FriendsAccepted { get; set; } = [];
        public ICollection<Friend> FriendsRequested { get; set; } = [];
    }
}
        //public string? ProfilePictureUrl { get; set; }
        //public required string FirstName { get; set; }
        //public required string LastName { get; set; }
        //public required string Email { get; set; }
        //public required string PhoneNumber { get; set; }
        //public string? UserName { get; set; }
        //public string? Address { get; set; }
        //public required string Gender { get; set; }
        //public required DateOnly DateOfBirth { get; set; }
        //public string? Bio {  get; set; }
        //public ICollection<ChatMediaUser> ChatMediaUsers { get; set; } = [];
        //public ICollection<Post> MediaUserPosts { get; set; } = [];
        //public ICollection<PostMedia> MediaUserPostMedias { get; set; } = [];
        //public ICollection<PostComment> MediaUserComments { get; set; } = [];
        //public ICollection<ReplyComment> MediaUserReplies { get; set; } = [];
        //public ICollection<PostReaction> MediaUserPostReacts { get; set; } = [];
        //public ICollection<CommentReaction> MediaUserCommentReacts { get; set; } = [];
        //public ICollection<ReplyReaction> MediaUserReplyReacts { get; set; } = [];
        //public ICollection<PostMediaReaction> MediaUserPostMediaReacts { get; set; } = [];
        //public ICollection<PostMediaComment> MediaUserPostMediaComments { get; set; } = [];
        //public ICollection<Friend> MediaUserAcceptedFriends { get; set; } = [];
        //public ICollection<Friend> MediaUserRequstedFriends { get; set; } = [];
        //public ICollection<Like> MediaUserLikes { get; set; } = [];
        //public ICollection<Reply> MediaUserReplies { get; set; } = [];