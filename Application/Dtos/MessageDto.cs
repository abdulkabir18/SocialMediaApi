using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public record MessageDto
    {
        public Guid Id { get; set; }
        public required string Text { get; set; }
        public required Guid ChatId { get; set; }
        public required Guid SenderId { get; set; }
        public required Guid ReciverId { get; set; }
        public DateTime DateCreated { get; set; }
        public required string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }

    public record MessageRequestModel
    {
        [Required]
        public required string Text { get; set; }
        [Required]
        public required Guid ChatId { get; set; }
        [Required]
        public required Guid SenderId { get; set; }
        [Required]
        public required Guid ReciverId { get; set; }
    }
}
