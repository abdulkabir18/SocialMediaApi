using Application.Dtos;

namespace Application.Interfaces.Services
{
    public interface ICommentService
    {
        Task<Result<CommentDto>> CommentPost(AddCommentRequestModel model);
        Task<Result<CommentDto?>> ViewComment(Guid id);
        Task<Result<CommentDto>> EditComment(EditCommentRequestModel edit);
        Task<Result<CommentDto>> DeleteComment(DeleteCommentRequestModel delete);
        Task<Result<ICollection<CommentDto>>> ViewAllComments(Guid postId);
    }
}
