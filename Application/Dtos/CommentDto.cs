using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public record CommentDto
    {
        public Guid Id { get; set; }
        public required string Text { get; set; }
        public required Guid PostId { get; set; }
        public required Guid CommenterId { get; set; }
        public DateTime DateCreated { get; set; }
        public required string CreatedBy { get; set; }
        public int LikeCount { get; set; }
    }

    public record AddCommentRequestModel
    {
        [Required]
        public required string Text { get; set; }
        [Required]
        public required Guid PostId { get; set; }
    }

    public record EditCommentRequestModel
    {
        public Guid CommentId { get; set; }
        public string? Text { get; set; }
        public Guid PostId { get; set; }
    }

    public record DeleteCommentRequestModel
    {
        [Required]
        public required Guid CommentId { get; set; }
    }
}


