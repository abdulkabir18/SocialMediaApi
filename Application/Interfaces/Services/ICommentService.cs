using Application.Dtos;

namespace Application.Interfaces.Services
{
    public interface ICommentService
    {
        Task<Result<CommentDto>> CommentPost(AddCommentRequestModel model);
        Task<Result<CommentDto?>> ViewComment(Guid id);
        Task<Result<ICollection<CommentDto>>> ViewAllComments(Guid postId);
    }
}
