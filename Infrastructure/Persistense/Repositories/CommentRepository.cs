using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistense.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistense.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ProjectContext _context;
        public CommentRepository(ProjectContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Comment comment)
        {
             await _context.Set<Comment>().AddAsync(comment);
        }

        public async Task<bool> CheckAsync(Guid id)
        {
            return await _context.Set<Comment>().AsNoTracking().AnyAsync(c => c.Id == id);
        }

        public async Task<int> CountAsync(Guid postId)
        {
            return await _context.Set<Comment>().AsNoTracking().CountAsync(c => c.PostId == postId);
        }

        public async Task<ICollection<Comment>> GetAllAsync(Expression<Func<Comment, bool>> expression)
        {
            return await _context.Set<Comment>().AsNoTracking().Where(expression).OrderBy(c => c.DateCreated).ToListAsync();
        }

        public async Task<Comment?> GetAsync(Guid id)
        {
            return await _context.Set<Comment>().AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Update(Comment comment)
        {
            _context.Set<Comment>().Update(comment);
        }
    }
}
