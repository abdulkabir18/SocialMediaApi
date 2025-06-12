using Application.Dtos;

namespace Application.Interfaces.CurrentUser
{
    public interface ICurrentUser
    {
        string GetCurrentUserEmail();
        Task<Result<MediaUserDto?>> GetCurrentMediaUser();
    }
}
