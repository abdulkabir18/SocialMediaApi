using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IReplyRepository
    {
        Task AddAsync(Reply reply);
        void Update(Reply reply);
        Task<Reply?> GetAsync(Guid id);
        Task<ICollection<Reply>> GetAllAsync(Expression<Func<Reply, bool>> expression);
    }
}
