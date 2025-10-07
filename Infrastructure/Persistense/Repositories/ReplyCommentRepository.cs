using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistense.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistense.Repositories
{
    public class ReplyCommentRepository : IReplyCommentRepository
    {
        private readonly ProjectContext _context;
        public ReplyCommentRepository(ProjectContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ReplyComment reply)
        {
            await _context.ReplyComments.AddAsync(reply);
        }

        public async Task<ICollection<ReplyComment>> GetAllAsync(Expression<Func<ReplyComment, bool>> expression)
        {
            return await _context.Set<ReplyComment>().AsNoTracking().Where(expression).OrderByDescending(r => r.DateCreated).ToListAsync();
        }

        public async Task<ReplyComment?> GetAsync(Guid id)
        {
            return await _context.Set<ReplyComment>().AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public void Update(ReplyComment reply)
        {
            _context.ReplyComments.Update(reply);
        }
    }
}
