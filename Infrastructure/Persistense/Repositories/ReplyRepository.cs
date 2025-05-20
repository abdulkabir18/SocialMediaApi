using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistense.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistense.Repositories
{
    public class ReplyRepository : IReplyRepository
    {
        private readonly ProjectContext _context;
        public ReplyRepository(ProjectContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Reply reply)
        {
            await _context.Set<Reply>().AddAsync(reply);
        }

        public void Delete(Reply reply)
        {
            _context.Set<Reply>().Remove(reply);
        }

        public async Task<ICollection<Reply>> GetAllAsync(Expression<Func<Reply, bool>> expression)
        {
            return await _context.Set<Reply>().AsNoTracking().Where(expression).OrderBy(r => r.DateCreated).ToListAsync();
        }

        public async Task<Reply?> GetAsync(Guid id)
        {
            return await _context.Set<Reply>().AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public void Update(Reply reply)
        {
            _context.Set<Reply>().Update(reply);
        }
    }
}
