using Application.Dtos;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<Result<string>> Login(LoginUserRequestModel model);
    }
}
