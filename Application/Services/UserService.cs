using Application.Dtos;
using Application.Interfaces.Auth;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<string> _passwordHasher;
        private readonly IAuthService _authService;
        public UserService(IUserRepository userRepository, IPasswordHasher<string> passwordHasher, IAuthService authService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _authService = authService;
        }
        public async Task<Result<string>> Login(LoginUserRequestModel model)
        {
            var user = await _userRepository.GetAsync(u => u.Email == model.Input.ToLower() || u.PhoneNumber == model.Input);
            if (user == null)
            {
                return new Result<string>
                {
                    Data = null,
                    Message = "Invalid credentials",
                    Status = false
                };
            }

            var verifyData = _passwordHasher.VerifyHashedPassword(user.Email.ToLower(),user.Password, $"{user.Salt}{model.Password}");
            if(verifyData == PasswordVerificationResult.Failed)
            {
                return new Result<string>
                {
                    Data = null,
                    Message = "Invalid credentials",
                    Status = false
                };
            }

            var userDto = new UserDto(user.Id, user.ImageUrl, user.Email, user.PhoneNumber);

            return new Result<string>
            {
                Data = _authService.GenerateToken(userDto),
                Message = "Login Successfull",
                Status = true
            };
        }
    }
}
