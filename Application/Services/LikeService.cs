using Application.Dtos;
using Application.Interfaces.CurrentUser;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;
using Domain.Entities;

namespace Application.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IUnitOfWork _unitOfWork;

        public LikeService(ILikeRepository likeRepository, IPostRepository postRepository,ICurrentUser currentUser, IUnitOfWork unitOfWork)
        {
            _likeRepository = likeRepository;
            _postRepository = postRepository;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<LikeDto?>> GetLike(Guid id)
        {
            var like = await _likeRepository.GetAsync(id);
            if(like == null)
            {
                return new Result<LikeDto?>
                {
                    Message = "Like Not found",
                    Data = null,
                    Status = false
                };
            }

            return new Result<LikeDto?>
            {
                Message = "Found",
                Data = new LikeDto { CreatedBy = like.CreatedBy, LikerId = like.LikerId, DateCreated = like.DateCreated, Id = like.Id, IsDeleted = like.IsDeleted, CommentId = like.CommentId, PostId = like.PostId, ReplyId = like.ReplyId },
                Status = true
            };
        }

        public async Task<Result<ICollection<LikeDto>>> GetLikes(Guid postId)
        {
            var likes = await _likeRepository.GetAllAsync(l => l.PostId == postId);

            if (likes.Count == 0)
            {
                return new Result<ICollection<LikeDto>>
                {
                    Message = "No like found",
                    Data = null,
                    Status = false
                };
            }

            ICollection<LikeDto> likesDto = [];

            foreach (var like in likes)
            {
                if(!like.IsDeleted)
                {
                    var likeDto = new LikeDto
                    {
                        CreatedBy = like.CreatedBy,
                        LikerId = like.LikerId,
                        IsDeleted = like.IsDeleted,
                        CommentId = like.CommentId,
                        PostId = like.PostId,
                        DateCreated = like.DateCreated,
                        DateModified = like.DateModified,
                        Id = like.Id,
                        ModifiedBy = like.ModifiedBy,
                        ReplyId = like.ReplyId
                    };
                    likesDto.Add(likeDto);
                }
            }

            if(likesDto.Count == 0)
            {
                return new Result<ICollection<LikeDto>>
                { Message = "No like avaliable", Data =  null, Status = false };
            }


            return new Result<ICollection<LikeDto>>
            {
                Message = "Likes loading...",
                Data = likesDto,
                Status = true
            };
        }

        public async Task<Result<LikeDto>> LikePost(LikeRequestModel model)
        {
            bool checkPost = await _postRepository.CheckAsync(model.PostId);
            var mediaUser = await _currentUser.GetCurrentMediaUser();
            if (mediaUser is not null && mediaUser.Data is not null && checkPost)
            {
                bool response = await ToogleLike(mediaUser.Data, model.PostId);

                if (response)
                {
                    return new Result<LikeDto>
                    {
                        Message = "Post like successfully",
                        Data = { },
                        Status = true
                    };
                }
                return new Result<LikeDto>
                {
                    Message = "Post unlike successfully",
                    Data = { },
                    Status = true
                };
            }
            return new Result<LikeDto>
            {
                Message = "Like failed",
                Data = null,
                Status = false
            };
        }

        public async Task<bool> ToogleLike(MediaUserDto mediaUser, Guid postId)
        {
            var _like = await _likeRepository.GetAsync(mediaUser.Id, postId);
            if (_like is null)
            {
                var like = new Like
                {
                    CreatedBy = mediaUser.FullName,
                    LikerId = mediaUser.Id,
                    PostId = postId
                };

                await _likeRepository.AddAsync(like);
                await _unitOfWork.SaveAsync();

                return true;
            }

            _like.IsDeleted = !_like.IsDeleted;
            _likeRepository.Update(_like);
            await _unitOfWork.SaveAsync();
            return false;
        }
    }
}
