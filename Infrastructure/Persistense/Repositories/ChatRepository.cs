using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistense.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistense.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ProjectContext _context;
        public ChatRepository(ProjectContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Chat chat)
        {
            await _context.Set<Chat>().AddAsync(chat);
        }

        public async Task<ICollection<Chat>> GetAllAsync(Guid mediaUserId)
        {
            return await _context.Set<Chat>().AsNoTracking().Where(c => c.ChatMediaUsers.Any(cm => cm.MediaUserId == mediaUserId)).OrderBy(p => p.ChatMessages.OrderBy(m => m.DateCreated)).Include(m => m.MediaUser.AcceptedFriends).ToListAsync();
        }

        public async Task<Chat?> GetAsync(Guid id)
        {
            return await _context.Set<Chat>().AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Update(Chat chat)
        {
            _context.Set<Chat>().Update(chat);
        }
    }
}
