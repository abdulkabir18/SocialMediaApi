using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IMessageRepository
    {
        Task AddAsync(Message message);
        void Update(Message message);
        Task<Message?> GetAsync(Guid id);
        Task<ICollection<Message>> GetAllAsync(Expression<Func<Message, bool>> expression);
    }
}
