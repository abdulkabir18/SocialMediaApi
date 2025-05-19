using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistense.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistense.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ProjectContext _context;
        public LikeRepository(ProjectContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Like like)
        {
            await _context.Set<Like>().AddAsync(like);
        }

        public async Task<int> CountAsync(Expression<Func<Like, bool>> expression)
        {
            return await _context.Set<Like>().AsNoTracking().CountAsync(expression);
        }

        public async Task<ICollection<Like>> GetAllAsync(Expression<Func<Like, bool>> expression)
        {
            return await _context.Set<Like>().AsNoTracking().Where(expression).OrderBy(l => l.DateCreated).ToListAsync();
        }

        public async Task<Like?> GetAsync(Guid id)
        {
            return await _context.Set<Like>().AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Like?> GetAsync(Guid mediaUserId, Guid postId)
        {
            return await _context.Set<Like>().AsNoTracking().FirstOrDefaultAsync(l => l.LikerId == mediaUserId && l.PostId == postId);
        }

        public void Update(Like like)
        {
            _context.Set<Like>().Update(like);
        }
    }
}
