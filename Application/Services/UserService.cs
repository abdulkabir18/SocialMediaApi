using System.Text.RegularExpressions;
using Application.Dtos;
using Application.Interfaces.Auth;
using Application.Interfaces.CurrentUser;
using Application.Interfaces.External;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediaUserRepository _mediaUserRepository;
        private readonly IPasswordHasher<string> _passwordHasher;
        private readonly ILogger<UserService> _logger;
        private readonly IAuthService _authService;
        private readonly IMemoryCache _memoryCache;
        private readonly IEmailSenderService _emailSender;
        private readonly ICurrentUser _currentUser;
        private readonly ICurrentDeviceService _currentDeviceService;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUserRepository userRepository, ILogger<UserService> logger, ICurrentDeviceService currentDeviceService, ICurrentUser currentUser, IMediaUserRepository mediaUserRepository, IPasswordHasher<string> passwordHasher, IAuthService authService, IMemoryCache memoryCache, IEmailSenderService emailSender, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mediaUserRepository = mediaUserRepository;
            _currentDeviceService = currentDeviceService;
            _currentUser = currentUser;
            _passwordHasher = passwordHasher;
            _authService = authService;
            _memoryCache = memoryCache;
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
        }

        private static bool IsLocalIp(string ip)
        {
            return ip.StartsWith("127.") || ip.Equals("::1") ||
                   ip.StartsWith("192.168.") || ip.StartsWith("10.");
        }

        public async Task<Result<ForgottenPasswordConfirmation?>> ForgotPassword(ForgotPasswordRequest model)
        {
            string emailRegex = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            string phonePattern = @"^(070|080|081|090|091)\d{8}$";

            if (Regex.IsMatch(model.Input, emailRegex))
            {
                var record = await _mediaUserRepository.GetAsync(m => m.Email == model.Input && !m.IsDeleted);
                if (record == null) return new Result<ForgottenPasswordConfirmation?> { Message = $"We couldn't find your account that is associate with this email {model.Input}. Try again!!!", Data = null, Status = false };

                return new Result<ForgottenPasswordConfirmation?>
                { Message = "Found user", Data = new ForgottenPasswordConfirmation { Email = record.Email, FullName = record.FirstName + " " + record.LastName, ProfilePictureUrl = record.ProfilePictureUrl }, Status = true };
            }
            else if(Regex.IsMatch(model.Input, phonePattern))
            {
                var record = await _mediaUserRepository.GetAsync(m => m.PhoneNumber == model.Input && !m.IsDeleted);
                if (record == null) return new Result<ForgottenPasswordConfirmation?> { Message = $"We couldn't find your account that is associate with this number {model.Input}. Try again!!!", Data = null, Status = false };

                return new Result<ForgottenPasswordConfirmation?>
                { Message = "Found user", Data = new ForgottenPasswordConfirmation { Email = record.Email, FullName = record.FirstName + " " + record.LastName, ProfilePictureUrl = record.ProfilePictureUrl }, Status = true };
            }

            return new Result<ForgottenPasswordConfirmation?>
            { Message = "No search results\nYour search did not return any result. Please try again with other information.", Data = null, Status = false };
        }

        public async Task<Result<string>> Login(LoginUserRequestModel model)
        {
            var user = await _userRepository.GetAsync(u => (u.Email == model.Input.ToLower() && u.IsDeleted) || (u.PhoneNumber == model.Input && !u.IsDeleted));
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

            var ip = _currentDeviceService.GetClientIp();
            if(!string.IsNullOrEmpty(ip) && !IsLocalIp(ip))
            {
                var isFirstLogin = string.IsNullOrWhiteSpace(user.LoggedInDeviceIpAddress);
                var isNewDevice = user.LoggedInDeviceIpAddress != ip && !isFirstLogin;

                if(isFirstLogin || isNewDevice)
                {
                    //user.LoggedInDeviceIpAddress = ip;

                    if(isNewDevice && user.IsEmailVerified)
                    {
                        var location = await _currentDeviceService.GetLocationAsync(ip);
                        var timeUtc = DateTime.UtcNow.ToString("yyyy-mm-dd:HH:mm:ss 'UTC'");

                        var body = $@"
Hi User,

We detected a new login to your Kabivo account from an unrecognized device or location:

- IP Address: {ip}
- Location: near {location?.City ?? "Unknown"}
- Time (UTC): {timeUtc}

If this was you, you can safely ignore this message.

If you don’t recognize this activity, please change your password immediately from your account settings.

Stay safe,  
The Kabivo Security Team
";

                        try
                        {
                            await _emailSender.SendMesage("Dear user", user.Email, "Security Alert: New Login to Your Kabivo Account", body);
                            _logger.LogInformation("Security alert email sent to {Email} for new device login.", user.Email);
                        }
                        catch(Exception ex) 
                        {
                            _logger.LogError(ex, "Failed to send security alert email to {Email}", user.Email);
                        }
                    }

                    if (isFirstLogin || isNewDevice)
                    {
                        user.LoggedInDeviceIpAddress = ip;
                    }
                }
            }

            user.LogInAt = DateTime.UtcNow;
            _userRepository.Update(user);
            await _unitOfWork.SaveAsync();

            //if(user.LoggedInDeviceIpAddress == null && ip != string.Empty)
            //{
            //    user.LoggedInDeviceIpAddress = ip;
            //}
            //else if(user.LoggedInDeviceIpAddress != ip && user.IsEmailVerified)
            //{
            //    user.LoggedInDeviceIpAddress = ip;
            //    var location = await _currentDeviceService.GetLocationAsync(ip);
            //    string body = $"Hi User,\n\nSomeone just logged in to your Kapo account at {DateTime.UtcNow}. If this wasn't you, we're here to help you take some simple steps to secure your account\nBy resetting your password\n\nThanks,\nKapo security";
            //    await _emailSender.SendMesage("Dear user", user.Email, "Security alert", body);

            //}

            var userDto = new UserDto(user.Id, user.Email, user.PhoneNumber);

            return new Result<string>
            {
                Data = _authService.GenerateToken(userDto),
                Message = "Login Successfull",
                Status = true
            };
        }

        public async Task<Result<string>> ResetPassword(ResetPasswordRequestModel model)
        {
            if(model.NewPassword != model.NewPasswordConfirmation ) return new Result<string> { Message = "Password not match", Data = null, Status = false };

            var record = await _userRepository.GetAsync(model.UserId);
            var currentEmail = _currentUser.GetCurrentUserEmail();
            if (record == null || string.IsNullOrWhiteSpace(currentEmail) || record.Email != currentEmail) return new Result<string> { Message = "Failed to get record", Data =  null, Status = false };
            var verifyData = _passwordHasher.VerifyHashedPassword(record.Email.ToLower(), record.Password, $"{record.Salt}{model.CurrentPassword}");
            if (verifyData == PasswordVerificationResult.Failed) return new Result<string> { Message = "Invalid password", Data = null, Status = false };

            var salt = Guid.NewGuid().ToString();
            record.Password = _passwordHasher.HashPassword(record.Email.ToLower(), salt + model.NewPassword);
            record.Salt = salt;

            _userRepository.Update(record);
            await _unitOfWork.SaveAsync();

            var body = $@"
Hi User,

This is a confirmation that your Kabivo account password was successfully changed.

If you made this change, no further action is needed.

If you did **not** change your password, your account may be compromised. Please reset your password immediately and contact our support team.

Stay safe,  
The Kabivo Security Team
";

            try
            {
                await _emailSender.SendMesage("Dear user", record.Email, "Security Alert: Your Kabivo Password Was Reset", body);
                _logger.LogInformation("Password reset email sent to {Email} for UserId {Id}.", record.Email, record.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send password reset confirmation email to {Email} for UserId {Id}", record.Email, record.Id);
            }

            _logger.LogInformation("User {Email} reset password successfully.", record.Email);

            return new Result<string> { Message = "Password reset successfully", Data = record.Email, Status = true };
        }

        public async Task<Result<string>> VerifyEmail(string email, string code)
        {
            var user = await _userRepository.GetAsync(u => u.Email == email.ToLower());
            if(user == null) return new Result<string> { Message = "Invalid Email", Data = null, Status = false };

            if (!int.TryParse(code, out int codeValue)) return new Result<string> { Message = "Invalid input", Status = false, Data = null };

            if (!_memoryCache.TryGetValue(email.ToLower(), out int cachedValue)) return new Result<string> { Message = "Verification code expired, Try again", Data = null, Status = false };
            if (cachedValue != codeValue) return new Result<string> { Message = "Invalid verification code", Status = false, Data = null };

            user.IsEmailVerified = true;
            _userRepository.Update(user);
            await _unitOfWork.SaveAsync();

            _memoryCache.Remove(email.ToLower());
            _logger.LogInformation("User {Email} successfully verified email.", user.Email);

            return new Result<string> { Message = "Email verified successfully", Data = email, Status = true };
        }
    }
}
