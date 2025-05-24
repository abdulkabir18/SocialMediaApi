using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces.CurrentUser;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;
using Domain.Entities;

namespace Application.Services
{
    public class FriendService : IFriendService
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IMediaUserRepository _mediaUserRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        public FriendService(IFriendRepository friendRepository, IMediaUserRepository mediaUserRepository, IUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            _friendRepository = friendRepository;
            _mediaUserRepository = mediaUserRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }
        public async Task<Result<FriendDto>> AddFriend(AddFriendRequestModel model)
        {
            bool checkUser = await _mediaUserRepository.CheckAsync(m => m.Id == model.AddresseeId && !m.IsDeleted);
            if(!checkUser)  return new Result<FriendDto>
                { Message = "Error: You can add this person", Data = null, Status = false };
            
            var  mediaUser = await _currentUser.GetCurrentMediaUser();
            if(mediaUser.Data == null || mediaUser.Data.Id != model.RequesterId)
                return new Result<FriendDto>
                { Message = "Error: Verification failed",Data = null, Status = false };

            var friend = new Friend
            {
                CreatedBy = mediaUser.Data.FullName,
                AddresseeId = model.AddresseeId,
                RequesterId = model.RequesterId,
            };

            await _friendRepository.AddAsync(friend);
            await _unitOfWork.SaveAsync();

            return new Result<FriendDto>
            { Message = "Request sent", Data = new FriendDto { Id = friend.Id, AddresseeId = friend.AddresseeId, RequesterId = friend.RequesterId, DateCreated = friend.DateCreated, Status = friend.Status }, Status = true };
        }
    }
}
