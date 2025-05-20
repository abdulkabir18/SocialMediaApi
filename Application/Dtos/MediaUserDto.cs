using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.Dtos
{
    public record MediaUserDto
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public string? UserName { get; set; }
        public string? Address { get; set; }
        public required string Gender { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public DateTime DateCreated { get; set; }
        public required string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
    public record RegisterRequestModel
    {
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        public string? UserName { get; set; }
        public string? Address { get; set; }
        [Required]
        [MaxLength(7)]
        public required string Gender { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public required DateOnly DateOfBirth { get; set; }
        [Required]
        [Phone]
        public required string PhoneNumber { get; set; }
        [Required]
        [MinLength(8)]
        public required string Password { get; set; }
        [Required]
        [Compare("Password")]
        public required string ConfirmPassword { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }

    public record EditRequestModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Address { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }

    public record DeleteAccountRequestModel
    {
        [Required]
        public required Guid MediaUserId { get; set; }
    }

}
