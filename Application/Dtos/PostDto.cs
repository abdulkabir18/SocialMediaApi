using System.ComponentModel.DataAnnotations;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Dtos
{
    public record PostDto
    {
        public Guid Id { get; set; }
        public string? ContentText { get; set; }
        public string? ContentUrl { get; set; }
        public required Guid PosterId { get; set; }
        public DateTime DateCreated { get; set; }
        public required string CreatedBy { get; set; }
        public required PostVisibility PostVisibility { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
    }

    public record MakePostRequestModel
    {
        public string? ContentText { get; set; }
        public IFormFileCollection Files { get; set; } = default!;
        [Required]
        public required PostVisibility PostVisibility { get; set; }

        //[Required]
        //public required string Title { get; set; }
        //[Required]
        //public required ContentType ContentType { get; set; }
        //public string? ContentText { get; set; }
        //public IFormFile? Content { get; set; }
    }

    public record EditPostRequestModel
    {
        [Required]
        public required Guid PostId { get; set; }
        public required PostVisibility PostVisibility { get; set; }

        //public string? Title { get; set; }
        //[Required]
        //public required ContentType ContentType { get; set; }
        //public string? ContentText { get; set; }
        //public IFormFile? Content { get; set; }
    }

    public record DeletePostRequestModel
    {
        [Required]
        public required Guid PostId { get; set; }
    }
}
