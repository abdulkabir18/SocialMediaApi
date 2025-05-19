using Application.Dtos;
using Application.Interfaces.External;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class MediaUserService : IMediaUserService
    {
        private readonly IMediaUserRepository _mediaUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<string> _passwordHasher;
        private readonly IUploadService _uploadService;
        public MediaUserService(IMediaUserRepository mediaUserRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher<string> passwordHasher, IUploadService uploadService)
        {
            _mediaUserRepository = mediaUserRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _uploadService = uploadService;
        }
        public async Task<Result<MediaUserDto>> RegisterUser(RegisterRequestModel model)
        {
            bool checkEmail = await _mediaUserRepository.CheckAsync(m => m.Email == model.Email);
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

            var salt = Guid.NewGuid().ToString();
            var user = new User
            {
                CreatedBy = model.Email.ToLower(),
                Email = model.Email.ToLower(),
                Password = _passwordHasher.HashPassword(model.Email.ToLower(), salt + model.Password),
                PhoneNumber = model.PhoneNumber,
                Salt = salt,
                ImageUrl = model.ProfilePicture is not null ? await _uploadService.UploadProfileImageAsync(model.ProfilePicture)! : null
            };

            var mediaUser = new MediaUser
            {
                CreatedBy = model.Email.ToLower(),
                DateOfBirth = model.DateOfBirth,
                Email = model.Email.ToLower(),
                FirstName = model.FirstName,
                Gender = model.Gender.ToUpper(),
                LastName = model.LastName,
                Address = model.Address,
                UserName = model.UserName
            };

            await _userRepository.AddAsync(user);
            await _mediaUserRepository.AddAsync(mediaUser);
            await _unitOfWork.SaveAsync();

            return new Result<MediaUserDto>
            {
                Data = new MediaUserDto {CreatedBy = mediaUser.CreatedBy,DateOfBirth = mediaUser.DateOfBirth,Email = mediaUser.Email,FullName = mediaUser.FirstName + " " + mediaUser.LastName, Gender = mediaUser.Gender,Id = mediaUser.Id, Address = mediaUser.Address, UserName = mediaUser.UserName},
                Message = "Account Created successfull",
                Status = true
            };
        }
    }
}
