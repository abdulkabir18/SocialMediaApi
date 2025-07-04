﻿using Application.Dtos;

namespace Application.Interfaces.Services
{
    public interface IMediaUserService 
    {
        Task<Result<MediaUserDto>> RegisterUser(RegisterRequestModel  model);
        Task<Result<MediaUserDto>> UserProfile();
        Task<Result<MediaUserDto>> UserProfile(Guid mediaUserId);
        //Task<Result<MediaUserDto>> EditDetails(EditRequestModel edit);
        //Task<Result<MediaUserDto>> DeleteAccount(DeleteAccountRequestModel delete);
    }
}