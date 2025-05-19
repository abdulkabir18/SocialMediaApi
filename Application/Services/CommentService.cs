using Application.Dtos;
using Application.Interfaces.CurrentUser;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;
using Domain.Entities;

namespace Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(ICommentRepository commentRepository, IPostRepository postRepository, ILikeRepository likeRepository, ICurrentUser currentUser, IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _likeRepository = likeRepository;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<CommentDto>> CommentPost(AddCommentRequestModel model)
        {
            var checkPost = await _postRepository.CheckAsync(model.PostId);
            if (checkPost)
            {
                var mediaUser = await _currentUser.GetCurrentMediaUser();
                if (mediaUser.Data != null)
                {
                    var comment = new Comment
                    {
                        CommenterId = mediaUser.Data.Id,
                        CreatedBy = mediaUser.Data.FullName,
                        PostId = model.PostId,
                        Text = model.Text
                    };

                    await _commentRepository.AddAsync(comment);
                    await _unitOfWork.SaveAsync();

                    return new Result<CommentDto>
                    {
                        Message = "Post commented successfully",
                        Data = new CommentDto { CommenterId = comment.CommenterId, CreatedBy = comment.CreatedBy, PostId = comment.PostId, Text = comment.Text, DateCreated = comment.DateCreated, Id = comment.Id, IsDeleted = comment.IsDeleted, LikeCount = 0 },
                        Status = true
                    };
                }
            }

            return new Result<CommentDto>
            {
                Message = "Comment creation failed",
                Data = null,
                Status = false
            };
        }
        public async Task<Result<ICollection<CommentDto>>> ViewAllComments(Guid postId)
        {
            var comments = await _commentRepository.GetAllAsync(c => c.PostId == postId);
            if(comments.Count == 0)
            {
                return new Result<ICollection<CommentDto>>
                {
                    Message = "No comment has been made",
                    Data = null,
                    Status = false
                };
            }

            ICollection<CommentDto> commentDtos = [];
            foreach (var comment in comments)
            {
                if(!comment.IsDeleted)
                {
                    var commentDto = new CommentDto
                    {
                        CommenterId = comment.CommenterId,
                        CreatedBy = comment.CreatedBy,
                        PostId = comment.PostId,
                        Text = comment.Text,
                        DateCreated = comment.DateCreated,
                        Id = comment.Id,
                        IsDeleted = comment.IsDeleted,
                        LikeCount = await _likeRepository.CountAsync(l => l.CommentId == comment.Id)
                    };
                    commentDtos.Add(commentDto);
                }
            }

            if(commentDtos.Count == 0)
                return new Result<ICollection<CommentDto>> { Message = "No comment avaliable", Data = null, Status = false };

            return new Result<ICollection<CommentDto>>
            { Message = "Comments loading...",Data = commentDtos, Status = true };
        }

        public async Task<Result<CommentDto?>> ViewComment(Guid id)
        {
            var comment = await _commentRepository.GetAsync(id);
            if(comment == null)
            {
                return new Result<CommentDto?>
                { Message = "Comment not found", Data= null, Status = false };
            }

            return new Result<CommentDto?>
            {
                Message = "Found",
                Data = new CommentDto
                {
                    CommenterId = comment.CommenterId,
                    CreatedBy = comment.CreatedBy,
                    PostId = comment.PostId,
                    Text = comment.Text,
                    DateCreated = comment.DateCreated,
                    Id = comment.Id,
                    IsDeleted = comment.IsDeleted,
                    LikeCount = await _likeRepository.CountAsync(l => l.CommentId == comment.Id)
                },
                Status = true
            };
        }
    }
}
