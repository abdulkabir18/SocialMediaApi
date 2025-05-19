namespace Application.Dtos
{
    public record ChatDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }

    public record StartChatRequestModel
    {
        public required Guid MediaUserId { get; set; }
    }

}
