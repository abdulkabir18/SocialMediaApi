namespace Domain.Entities
{
    public class User : AuditableEntity
    {
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Salt { get; set; }
        public string? LoggedInDeviceIpAddress { get; set; }
        public DateTime? LogInAt { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsPhoneNumberVerified { get; set; }
    }
}