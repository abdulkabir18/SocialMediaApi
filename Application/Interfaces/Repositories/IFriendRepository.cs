using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IFriendRepository
    {
        Task AddAsync(Friend friend);
        void Update(Friend friend);
        Task<Friend?> GetAsync(Guid id);
        Task<Friend?> GetAsync(Expression<Func<Friend, bool>> expression);
        Task<int> CountAsync(Expression<Func<Friend, bool>> expression);
        Task<ICollection<Friend>> GetAllAsync(Expression<Func<Friend, bool>> expression);
        Task<ICollection<Friend>> GetAllAsync();
    }
}
