using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IPostCommentRepository
    {
        Task AddAsync(PostComment comment);
        void Update(PostComment comment);
        Task<PostComment?> GetAsync(Guid id);
        Task<int> CountAsync(Guid postId);
        Task<bool> CheckAsync(Guid id);
        Task<ICollection<PostComment>> GetAllAsync(Expression<Func<PostComment, bool>> expression);
    }
}
