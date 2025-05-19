using Application.Dtos;

namespace Application.Interfaces.Services
{
    public interface ILikeService
    {
        Task<Result<LikeDto>> LikePost(LikeRequestModel model);
        Task<Result<LikeDto?>> GetLike(Guid id);
        Task<bool> ToogleLike(MediaUserDto mediaUser, Guid PostId);
        Task<Result<ICollection<LikeDto>>> GetLikes(Guid postId);
    }
}
