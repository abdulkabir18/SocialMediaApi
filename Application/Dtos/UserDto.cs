namespace Application.Dtos
{
    public record UserDto (Guid Id, string? ImageUrl, string Email, string PhoneNumber);
    public record LoginUserRequestModel(string Input,  string Password);
        
}
