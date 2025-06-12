using System.Security.Claims;
using Application.Dtos;
using Application.Interfaces.CurrentUser;
using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Service.CurrentUser
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMediaUserRepository _mediaUserRepository;
        public CurrentUser(IHttpContextAccessor contextAccessor, IMediaUserRepository mediaUserRepository)
        {
            _contextAccessor = contextAccessor;
            _mediaUserRepository = mediaUserRepository;
        }

        public async Task<Result<MediaUserDto?>> GetCurrentMediaUser()
        {
            string userEmail = GetCurrentUserEmail();
            if(userEmail != null)
            {
                var mediaUser = await _mediaUserRepository.GetAsync(m => m.Email == userEmail && !m.IsDeleted);
                if(mediaUser != null)
                {
                    return new Result<MediaUserDto?>
                    {
                        Message = "Retrived data",
                        Data = new MediaUserDto {DateOfBirth = mediaUser.DateOfBirth, Email = mediaUser.Email, FullName = mediaUser.FirstName + " " + mediaUser.LastName, Gender = mediaUser.Gender, Address = mediaUser.Address, Id = mediaUser.Id, DateJoined = mediaUser.DateCreated, UserName = mediaUser.UserName },
                        Status = true
                    };
                }
            }
            return new Result<MediaUserDto?>
            {
                Message = "No data",
                Data = null,
                Status = false
            };
        }

        public string GetCurrentUserEmail()
        {
            //var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var userEmail = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)!.Value;

            //return [userId, userEmail];
            return userEmail;
        }
    }
}
