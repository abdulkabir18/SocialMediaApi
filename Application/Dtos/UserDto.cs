using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public record UserDto (Guid Id, string Email, string PhoneNumber);
    public record LoginUserRequestModel(string Input,  string Password);
    public record ResetPasswordRequestModel
    {
        [Required]
        public required Guid UserId { get; set; }
        [Required]
        [PasswordPropertyText]
        [MinLength(8)]
        public required string CurrentPassword { get; set; }
        [Required]
        [PasswordPropertyText]
        [MinLength(8)]
        public required string NewPassword { get; set; }
        [Required]
        [Compare("Password")]
        public required string NewPasswordConfirmation { get; set; }
    }
    public record ForgottenPasswordConfirmation
    {
        public string? ProfilePictureUrl { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
    }

    public record ForgotPasswordRequest
    {
        public string Input { get; set; } = string.Empty;
    }
}
