using Application.Dtos;

namespace Application.Interfaces.Services
{
    public interface IReplyService
    {
        Task<Result<ReplyDto>> AddReply(AddReplyRequestModel model);
        Task<Result<ReplyDto>> GetReply(Guid id);
        Task<Result<ICollection<ReplyDto>>> GetReplies(Guid commentId);
    }
}
