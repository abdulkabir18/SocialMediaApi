namespace Domain.Entities
{
    public class User : AuditableEntity
    {
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Salt { get; set; }
        public string? ImageUrl {  get; set; }
    }
}
