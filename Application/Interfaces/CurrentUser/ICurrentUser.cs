using Application.Dtos;

namespace Application.Interfaces.CurrentUser
{
    public interface ICurrentUser
    {
        string GetCurrentUser();
        Task<Result<MediaUserDto?>> GetCurrentMediaUser();
    }
}
