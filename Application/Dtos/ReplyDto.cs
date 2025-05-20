using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public record ReplyDto
    {
        public Guid Id { get; set; }
        public required string Text { get; set; }
        public required Guid ReplyerId { get; set; }
        public required Guid CommentId { get; set; }
        public DateTime DateCreated { get; set; }
        public required string CreatedBy { get; set; }
        public int LikeCount { get; set; }
    }

    public record AddReplyRequestModel
    {
        [Required]
        public required string Text { get; set; }
        [Required]
        public required Guid CommentId { get; set; }
    }

    public record EditReplyRequestModel
    {
        public string? Text { get; set; }
        public Guid CommentId { get; set; }
        public Guid ReplyId { get; set; }
    }

    public record DeleteReplyRequestModel
    {
        public required Guid ReplyId { get; set; }
    }
}
