using Application.Dtos;
using Application.Interfaces.CurrentUser;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;
using Domain.Entities;

namespace Application.Services
{
    public class ReplyService : IReplyService
    {
        private readonly IReplyRepository _replyRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IUnitOfWork _unitOfWork;

        public ReplyService(IReplyRepository replyRepository, ICommentRepository commentRepository, ILikeRepository likeRepository, ICurrentUser currentUser, IUnitOfWork unitOfWork)
        {
            _replyRepository = replyRepository;
            _commentRepository = commentRepository;
            _likeRepository = likeRepository;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<ReplyDto>> AddReply(AddReplyRequestModel model)
        {
            bool checkComment = await _commentRepository.CheckAsync(model.CommentId);
            var currentUser = await _currentUser.GetCurrentMediaUser();
            if(checkComment && currentUser.Data != null)
            {
                var reply = new Reply
                {
                    CommentId = model.CommentId,
                    Text = model.Text,
                    CreatedBy = currentUser.Data.FullName,
                    ReplyerId = currentUser.Data.Id
                };
                await _replyRepository.AddAsync(reply);
                await _unitOfWork.SaveAsync();

                return new Result<ReplyDto>
                { Message = "Reply added successfully", Data = new ReplyDto { CommentId = reply.CommentId, Text = reply.Text,CreatedBy = reply.CreatedBy,ReplyerId = reply.ReplyerId,DateCreated = reply.DateCreated,Id = reply.Id,LikeCount = await _likeRepository.CountAsync(l => l.ReplyId == reply.Id && l.IsDeleted != true)}, Status = true };
            }
            return new Result<ReplyDto>
            { Message = "Reply creation failed", Status = false, Data = null };
        }

        public async Task<Result<ReplyDto>> DeleteReply(DeleteReplyRequestModel model)
        {
            var reply = await _replyRepository.GetAsync(model.ReplyId);
            if(reply == null) return new Result<ReplyDto> { Message = "Failed", Data=null, Status = false };

            var mediaUser = await _currentUser.GetCurrentMediaUser();
            if (mediaUser.Data == null || mediaUser.Data.Id != reply.ReplyerId) return new Result<ReplyDto> { Message = "Error: Record not found", Data = null, Status = false };

            _replyRepository.Delete(reply);
            await _unitOfWork.SaveAsync();

            return new Result<ReplyDto> { Message = "Deleted successfully", Data = { }, Status = true };
        }

        public async Task<Result<ReplyDto>> EditReply(EditReplyRequestModel model)
        {
            var checkComment = await _commentRepository.CheckAsync(model.CommentId);
            if(!checkComment)
            {
                return new Result<ReplyDto> { Message = "Error: Action rejected", Data = null, Status = false };
            }

            var reply = await _replyRepository.GetAsync(model.ReplyId);
            var mediaUser = await _currentUser.GetCurrentMediaUser();
            if (reply == null || mediaUser.Data == null) return new Result<ReplyDto> { Message = "Error: Failed to get some datails", Data = null, Status = false };

            if(reply.ReplyerId != mediaUser.Data.Id) return new Result<ReplyDto> { Message = "Error: No match record found", Data = null, Status = false };

            if(!string.IsNullOrEmpty(model.Text)) { reply.Text = model.Text; }
            else return new Result<ReplyDto> { Message = "No details to update with", Data = null, Status = false };

            reply.DateModified = DateTime.Now;
            reply.ModifiedBy = mediaUser.Data.FullName;
            _replyRepository.Update(reply);
            await _unitOfWork.SaveAsync();

            return new Result<ReplyDto> { Message = "Update successfully", Data = new ReplyDto { CommentId = reply.CommentId, CreatedBy = reply.ModifiedBy, ReplyerId = reply.ReplyerId, Text = reply.Text, DateCreated = reply.DateCreated, Id = reply.Id, LikeCount = await _likeRepository.CountAsync(r => r.ReplyId == reply.Id && r.IsDeleted != true) } };
        }

        public async Task<Result<ICollection<ReplyDto>>> GetReplies(Guid commentId)
        {
            var replies = await _replyRepository.GetAllAsync(r => r.CommentId == commentId);
            if(replies.Count == 0)
            {
                return new Result<ICollection<ReplyDto>> { Message = "No Reply has been added", Data= null, Status = false};
            }

            ICollection<ReplyDto> replyDtos = [];
            foreach(var reply in replies)
            {
                var replyDto = new ReplyDto
                {
                    CommentId = reply.CommentId,
                    Text = reply.Text,
                    CreatedBy = reply.CreatedBy,
                    ReplyerId = reply.ReplyerId,
                    DateCreated = reply.DateCreated,
                    Id = reply.Id,
                    LikeCount = await _likeRepository.CountAsync(l => l.CommentId == commentId && l.IsDeleted != true)
                };
                replyDtos.Add(replyDto);
            }

            return new Result<ICollection<ReplyDto>>
            { Message = "Reply loading...", Data =  replyDtos, Status = true };
        }

        public async Task<Result<ReplyDto>> GetReply(Guid id)
        {
            var reply = await _replyRepository.GetAsync(id);
            if(reply == null)
            {
                return new Result<ReplyDto>
                { Message = "Not found", Data = null, Status = false };
            }

            return new Result<ReplyDto>
            { Message = "Found", Data = new ReplyDto { CommentId = reply.CommentId, DateCreated = reply.DateCreated, Id = reply.Id,CreatedBy = reply.CreatedBy,ReplyerId = reply.ReplyerId,Text = reply.Text, LikeCount = await _likeRepository.CountAsync(l => l.ReplyId == reply.Id && l.IsDeleted != true) }, Status = true };
        }
    }
}
