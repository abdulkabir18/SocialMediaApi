using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Interfaces.Services
{
    public interface IFriendService
    {
        Task<Result<FriendDto>> AddFriend(AddFriendRequestModel model);
        Task<Result<FriendDto>> CancelFriendRequest()
    }
}
