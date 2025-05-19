using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Task AddAsync(Post post);
        void Update(Post post);
        Task<Post?> GetAsync(Guid id);
        Task<Post?> GetAsync(Expression<Func<Post, bool>> expression);
        Task<bool> CheckAsync(Guid id);
        Task<ICollection<Post>> GetAllAsync(Guid posterId);
        Task<ICollection<Post>> GetAllAsync();

    }
}
