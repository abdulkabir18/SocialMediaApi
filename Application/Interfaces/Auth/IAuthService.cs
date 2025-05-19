using Application.Dtos;

namespace Application.Interfaces.Auth
{
    public  interface IAuthService
    {
        string GenerateToken(UserDto model);
    }
}
