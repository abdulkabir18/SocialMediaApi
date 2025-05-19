using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IMediaUserRepository
    {
        Task AddAsync(MediaUser mediaUser);
        void Update(MediaUser mediaUser);
        Task<MediaUser?> GetAsync(Guid id);
        Task<bool> CheckAsync(Expression<Func<MediaUser, bool>> expression);
        Task<MediaUser?> GetAsync(Expression<Func<MediaUser, bool>> expression);
        Task<ICollection<MediaUser>> GetAllAsync();
    }
}
