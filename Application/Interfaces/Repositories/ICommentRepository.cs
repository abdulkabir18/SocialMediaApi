using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        Task AddAsync(Comment comment);
        void Update(Comment comment);
        Task<Comment?> GetAsync(Guid id);
        Task<int> CountAsync(Guid postId);
        Task<bool> CheckAsync(Guid id);
        Task<ICollection<Comment>> GetAllAsync(Expression<Func<Comment, bool>> expression);
    }
}
