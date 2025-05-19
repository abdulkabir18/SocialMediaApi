using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistense.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistense.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ProjectContext _context;
        public NotificationRepository(ProjectContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Notification notification)
        {
            await _context.Set<Notification>().AddAsync(notification);
        }

        public async Task<ICollection<Notification>> GetAllAsync(Guid mediaUserId)
        {
            return await _context.Set<Notification>().AsNoTracking().Where(n => n.MediaUserId == mediaUserId).ToListAsync();
        }

        public async Task<Notification?> GetAsync(Guid id)
        {
            return await _context.Set<Notification>().AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<Notification?> GetAsync(Expression<Func<Notification, bool>> expression)
        {
            return await _context.Set<Notification>().AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public void Update(Notification notification)
        {
            _context.Set<Notification>().Update(notification);
        }
    }
}
