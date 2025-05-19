using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification notification);
        void Update(Notification notification);
        Task<Notification?> GetAsync(Guid id);
        Task<Notification?> GetAsync(Expression<Func<Notification, bool>> expression);
        Task<ICollection<Notification>> GetAllAsync(Guid mediaUserId);
    }
}
