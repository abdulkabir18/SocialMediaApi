using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ILikeRepository
    {
        Task AddAsync(Like like);
        void Update(Like like);
        Task<Like?> GetAsync(Guid id);
        Task<Like?> GetAsync(Guid mediaUserId, Guid postId);
        Task<int> CountAsync(Expression<Func<Like, bool>> expression);
        Task<ICollection<Like>> GetAllAsync(Expression<Func<Like, bool>> expression);
    }
}
