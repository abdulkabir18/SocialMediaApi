namespace Application.Dtos
{
    public class Result<T>
    {
        public T? Data { get; set; } = default!;
        public required string Message { get; set; }
        public bool Status { get; set; }
    }
}
