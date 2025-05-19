using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistense.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistense.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ProjectContext _context;
        public MessageRepository(ProjectContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Message message)
        {
            await _context.Set<Message>().AddAsync(message);
        }

        public async Task<ICollection<Message>> GetAllAsync(Expression<Func<Message, bool>> expression)
        {
            return await _context.Set<Message>().AsNoTracking().Where(expression).OrderBy(m => m.DateCreated).ToListAsync();
        }

        public async Task<Message?> GetAsync(Guid id)
        {
            return await _context.Set<Message>().AsNoTracking().FirstOrDefaultAsync(m  => m.Id == id);
        }

        public void Update(Message message)
        {
            _context.Set<Message>().Update(message);
        }
    }
}
