using System.Text.RegularExpressions;
using Application.Dtos;
using Application.Interfaces.CurrentUser;
using Application.Interfaces.External;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Services
{
    public class MediaUserService : IMediaUserService
    {
        private readonly IMediaUserRepository _mediaUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly IFriendRepository _friendRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<string> _passwordHasher;
        private readonly IUploadService _uploadService;
        private readonly IEmailSenderService _emailSender;
        private readonly IMemoryCache _memoryCache;
        public MediaUserService(IMediaUserRepository mediaUserRepository, IFriendRepository friendRepository,IMemoryCache memoryCache, IUserRepository userRepository,IPostRepository postRepository, ICurrentUser currentUser, IUnitOfWork unitOfWork, IPasswordHasher<string> passwordHasher, IUploadService uploadService, IEmailSenderService emailSender)
        {
            _mediaUserRepository = mediaUserRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _friendRepository = friendRepository;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _uploadService = uploadService;
            _emailSender = emailSender;
            _memoryCache = memoryCache;
        }

        //public async Task<Result<MediaUserDto>> DeleteAccount(DeleteAccountRequestModel delete)
        //{
        //    var mediaUser = await _mediaUserRepository.GetAsync(delete.MediaUserId);
        //    if(mediaUser == null)
        //    {
        //        return new Result<MediaUserDto>
        //        {
        //            Message = "Account delete failed",
        //            Data = null,
        //            Status = false
        //        };
        //    }

        //    var user = await _userRepository.GetAsync(u => u.Email == mediaUser.Email);
        //    if(user == null)
        //    {
        //        return new Result<MediaUserDto>
        //        {
        //            Message = "Error: Delete failed",
        //            Data = null,
        //            Status = false
        //        };
        //    }

        //    var posts = await _postRepository.GetAllAsync(mediaUser.Id);
        //    if(posts.Count != 0)
        //    {
        //        foreach(var post in posts)
        //        {
        //            _postRepository.Delete(post);
        //            await _unitOfWork.SaveAsync();
        //        }
        //    }

        //    user.IsDeleted = true;
        //    mediaUser.IsDeleted = true;

        //    _userRepository.Update(user);
        //    _mediaUserRepository.Update(mediaUser);
        //    await _unitOfWork.SaveAsync();

        //    return new Result<MediaUserDto>
        //    {
        //        Message = "Account deleted successfully",
        //        Data = { },
        //        Status = true
        //    };
        //}

        //public async Task<Result<MediaUserDto>> EditDetails(EditRequestModel edit)
        //{
        //    var currentUserEmail = _currentUser.GetCurrentUserEmail();
        //    if (currentUserEmail == null) return new Result<MediaUserDto> { Message = "Error: Try again", Data = null, Status = false };

        //    var mediaUser = await _mediaUserRepository.GetAsync(m => m.Email == currentUserEmail);
        //    if (mediaUser == null) return new Result<MediaUserDto> { Message = "Error: Failed to edit details", Data = null, Status = false };

        //    if (!string.IsNullOrEmpty(edit.FirstName)) { mediaUser.FirstName = edit.FirstName; }

        //    if(!string.IsNullOrEmpty(edit.LastName)) { mediaUser.LastName = edit.LastName; }

        //    if(!string.IsNullOrEmpty(edit.UserName)) { mediaUser.UserName = edit.UserName; }

        //    if(!string.IsNullOrEmpty(edit.Address)) { mediaUser.Address = edit.Address; }

        //    var user = await _userRepository.GetAsync(u => u.Email == currentUserEmail);
        //    if (user == null) return new Result<MediaUserDto> { Message = "Error: Failed", Data = null, Status = false };

        //    //if(edit.ProfilePicture != null) { user.ImageUrl = await _uploadService.UploadProfileImageAsync(edit.ProfilePicture); }

        //    if (edit.ProfilePicture == null || (edit.LastName == null && edit.FirstName == null && edit.UserName == null && edit.Address == null))
        //        return new Result<MediaUserDto> { Message = "Details are required to edit", Data = null, Status = false };

        //    user.ModifiedBy = currentUserEmail;
        //    user.DateModified = DateTime.UtcNow;
        //    mediaUser.ModifiedBy = currentUserEmail;
        //    mediaUser.DateModified = DateTime.UtcNow;
        //    _userRepository.Update(user);
        //    _mediaUserRepository.Update(mediaUser);
        //    await _unitOfWork.SaveAsync();

        //    return new Result<MediaUserDto>
        //    {
        //        Message = "Details update successfully",
        //        Data = new MediaUserDto {DateOfBirth = mediaUser.DateOfBirth, Email = mediaUser.Email, FullName = mediaUser.FirstName + " " + mediaUser.LastName, Gender = mediaUser.Gender, Address = mediaUser.Address, DateJoined = mediaUser.DateCreated, Id = mediaUser.Id, UserName = mediaUser.UserName },
        //        Status = true
        //    };
        //}

        public async Task<Result<MediaUserDto>> RegisterUser(RegisterRequestModel model)
        {
            string emailRegex = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            if (!Regex.IsMatch(model.Email, emailRegex)) return new Result<MediaUserDto>
            { Message = "Invalid Email", Data = null, Status = false };

            bool checkEmail = await _mediaUserRepository.CheckAsync(m => m.Email == model.Email.ToLower());
            if(checkEmail)
            {
                return new Result<MediaUserDto>
                {
                    Data = null,
                    Message = $"Email: {model.Email} is already associated with another account",
                    Status = false
                };
            }

            if(!string.IsNullOrEmpty(model.UserName))
            {
                bool checkUsername = await _mediaUserRepository.CheckAsync(m => m.UserName == model.UserName);
                if(checkUsername)
                {
                    return new Result<MediaUserDto>
                    {
                        Data = null,
                        Message = $"UserName: {model.UserName} is already associated with another account",
                        Status = false
                    };
                }
            }

            if (model.Gender.ToLower() != "male" && model.Gender.ToLower() != "female")
            {
                return new Result<MediaUserDto>
                {
                    Data = null,
                    Message = $"Gender: Invalid input",
                    Status = false
                };
            }

            string phonePattern = @"^(070|080|081|090|091)\d{8}$";
            if(!Regex.IsMatch(model.PhoneNumber, phonePattern)) return new Result<MediaUserDto>
            { Message = "Invalid phone number", Data = null, Status = false };

            var checkPhonenumber = await _userRepository.CheckAsyncPhoneNumber(model.PhoneNumber);
            if(checkPhonenumber)
            {
                return new Result<MediaUserDto>
                {
                    Data = null,
                    Message = $"PhoneNumber: {model.PhoneNumber} is already associated with another account",
                    Status = false
                };
            }

            if(model.ProfilePicture != null)
            {
                var contentType = model.ProfilePicture.ContentType;
                if(!contentType.StartsWith("image/"))
                {
                    return new Result<MediaUserDto>
                    {
                        Data = null,
                        Message = $"ProfilePicture: Invalid input and you are requried to input image.",
                        Status = false
                    };
                }
            }

            string passwordRegex = @"^(?=.* [a - z])(?=.* [A - Z])(?=.*\d)(?=.* [\W_]).{ 8,}$";
            if (!Regex.IsMatch(passwordRegex, model.Password)) return new Result<MediaUserDto>
            { Message = "Weak password. Use upper, lower, number, special characters.", Data = null, Status = false };

            if (model.Password != model.ConfirmPassword) return new Result<MediaUserDto>
            { Message = "Passwords do not match ❌.", Status = false, Data = null };

            var salt = Guid.NewGuid().ToString();
            var user = new User
            {
                CreatedBy = model.Email.ToLower(),
                Email = model.Email.ToLower(),
                Password = _passwordHasher.HashPassword(model.Email.ToLower(), salt + model.Password),
                PhoneNumber = model.PhoneNumber,
                Salt = salt
            };

            int token = new Random().Next(100000,999999);
            _memoryCache.Set(user.Email, token,TimeSpan.FromSeconds(1200));
            await _emailSender.SendMesage(model.FirstName + " "+ model.LastName, user.Email, "Email Verification",token.ToString());

            var mediaUser = new MediaUser
            {
                Bio = model.Bio != string.Empty ? model.Bio : null,
                CreatedBy = model.Email.ToLower(),
                DateOfBirth = model.DateOfBirth,
                Email = model.Email.ToLower(),
                FirstName = model.FirstName,
                Gender = model.Gender.ToUpper(),
                LastName = model.LastName,
                Address = model.Address != string.Empty ? model.Address : null,
                UserName = model.UserName != string.Empty ? model.UserName : null,
                PhoneNumber = model.PhoneNumber,
                ProfilePictureUrl = model.ProfilePicture is not null ? await _uploadService.UploadProfileImageAsync(model.ProfilePicture) : null
            };

            await _userRepository.AddAsync(user);
            await _mediaUserRepository.AddAsync(mediaUser);
            await _unitOfWork.SaveAsync();

            return new Result<MediaUserDto>
            {
                Data = new MediaUserDto {DateOfBirth = mediaUser.DateOfBirth,Email = mediaUser.Email,FullName = mediaUser.FirstName + " " + mediaUser.LastName, Gender = mediaUser.Gender,Id = mediaUser.Id, Address = mediaUser.Address, UserName = mediaUser.UserName},
                Message = "Account Created successfull",
                Status = true
            };
        }

        public async Task<Result<MediaUserDto>> UserProfile()
        {
            string currentMediaUserEmail = _currentUser.GetCurrentUserEmail();
            if (string.IsNullOrEmpty(currentMediaUserEmail)) return new Result<MediaUserDto>
            { Message = "Retrive data failed", Data = null, Status = false };

            var mediaUser = await _mediaUserRepository.GetAsync(m => m.Email ==  currentMediaUserEmail && !m.IsDeleted);
            if(mediaUser == null) return new Result<MediaUserDto>
            { Message = "Retriving record failed", Data = null,Status = false };

            return new Result<MediaUserDto>
            { Message = "Profile found", Data = new MediaUserDto { DateOfBirth = mediaUser.DateOfBirth, Email = mediaUser.Email, FullName = mediaUser.FirstName + " " + mediaUser.LastName, Gender = mediaUser.Gender, Address = mediaUser.Address, Bio = mediaUser.Bio, DateJoined = mediaUser.DateCreated, Id = mediaUser.Id, ProfilePictureUrl = mediaUser.ProfilePictureUrl, UserName = mediaUser.UserName, FriendCount = await _friendRepository.CountAsync(f => f.AddresseeId == mediaUser.Id || f.RequesterId == mediaUser.Id && f.Status == FriendStatus.Accepted && !f.IsDeleted) }, Status = true };
        }

        public async Task<Result<MediaUserDto>> UserProfile(Guid mediaUserId)
        {
            var mediaUser = await _mediaUserRepository.GetAsync(mediaUserId);
            if(mediaUser == null) return new Result<MediaUserDto>
            { Message = "Record retrive failed", Data = null, Status =  false };

            if(mediaUser.IsDeleted) return new Result<MediaUserDto>
            { Message = "Record not available", Status = false, Data = null };

            return new Result<MediaUserDto>
            { Message = "Record found", Data = new MediaUserDto { DateOfBirth = mediaUser.DateOfBirth, Email = mediaUser.Email, FullName = mediaUser.FirstName + " " + mediaUser.LastName, Gender = mediaUser.Gender, Address = mediaUser.Address, Bio = mediaUser.Bio, DateJoined = mediaUser.DateCreated, ProfilePictureUrl = mediaUser.ProfilePictureUrl, Id = mediaUser.Id, UserName = mediaUser.UserName, FriendCount = await _friendRepository.CountAsync(f => f.AddresseeId == mediaUser.Id || f.RequesterId == mediaUser.Id && f.Status == FriendStatus.Accepted && !f.IsDeleted) }, Status = true };
        }
    }
}
