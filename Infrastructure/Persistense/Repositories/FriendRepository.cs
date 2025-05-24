using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistense.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistense.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly ProjectContext _context;
        public FriendRepository(ProjectContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Friend friend)
        {
            await _context.Set<Friend>().AddAsync(friend);
        }

        public async Task<int> CountAsync(Expression<Func<Friend, bool>> expression)
        {
            return await _context.Set<Friend>().AsNoTracking().CountAsync(expression);
        }

        public async Task<ICollection<Friend>> GetAllAsync()
        {
            return await _context.Set<Friend>().OrderByDescending(f => f.CreatedBy).ToListAsync();
        }

        public async Task<ICollection<Friend>> GetAllAsync(Expression<Func<Friend, bool>> expression)
        {
            return await _context.Set<Friend>().Where(expression).OrderByDescending(f => f.DateCreated).ToListAsync();
        }

        public async Task<Friend?> GetAsync(Guid id)
        {
            return await _context.Set<Friend>().AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Friend?> GetAsync(Expression<Func<Friend, bool>> expression)
        {
            return await _context.Set<Friend>().AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public void Update(Friend friend)
        {
            _context.Set<Friend>().Update(friend);
        }
    }
}
