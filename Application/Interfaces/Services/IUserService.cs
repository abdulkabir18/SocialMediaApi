using Application.Dtos;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<Result<string>> Login(LoginUserRequestModel model);
        Task<Result<string>> VerifyEmail(string email, string code);
        Task<Result<ForgottenPasswordConfirmation?>> ForgotPassword(ForgotPasswordRequest model);
        Task<Result<string>> ResetPassword(ResetPasswordRequestModel model);
    }
}
