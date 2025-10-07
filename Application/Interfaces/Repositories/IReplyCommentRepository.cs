using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IReplyCommentRepository
    {
        Task AddAsync(ReplyComment reply);
        void Update(ReplyComment reply);
        Task<ReplyComment?> GetAsync(Guid id);
        Task<ICollection<ReplyComment>> GetAllAsync(Expression<Func<ReplyComment, bool>> expression);
    }
}
