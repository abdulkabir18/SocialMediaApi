using System.ComponentModel.DataAnnotations;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Dtos
{
    public record PostDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required Guid PosterId { get; set; }
        public DateTime DateCreated { get; set; }
        public required string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
    }

    public record MakePostRequestModel
    {
        [Required]
        public required string Title { get; set; }
        [Required]
        public required ContentType ContentType { get; set; }
        public string? ContentText { get; set; }
        public IFormFile? Content { get; set; }
    }
}
