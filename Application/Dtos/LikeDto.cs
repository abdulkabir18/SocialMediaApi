using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public record LikeDto
    {
        public Guid Id { get; set; }
        public Guid? PostId { get; set; }
        public Guid? CommentId { get; set; }
        public Guid? ReplyId { get; set; }
        public required Guid LikerId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public required string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }

    public record LikeRequestModel
    {
        [Required]
        public Guid PostId { get; set; }
        //[Required]
        //public Guid? CommentId { get; set; }
        //[Required]
        //public Guid? ReplyId { get; set; }
    }
}
