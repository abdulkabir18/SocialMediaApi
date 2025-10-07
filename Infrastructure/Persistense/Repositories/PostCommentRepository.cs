using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistense.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistense.Repositories
{
    public class PostCommentRepository : IPostCommentRepository
    {
        private readonly ProjectContext _context;
        public PostCommentRepository(ProjectContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PostComment comment)
        {
            await _context.PostComments.AddAsync(comment);
        }

        public async Task<bool> CheckAsync(Guid id)
        {
            return await _context.PostComments.AsNoTracking().AnyAsync(c => c.Id == id);
        }

        public async Task<int> CountAsync(Guid postId)
        {
            return await _context.PostComments.AsNoTracking().CountAsync(c => c.PostId == postId);
        }

        public async Task<ICollection<PostComment>> GetAllAsync(Expression<Func<PostComment, bool>> expression)
        {
            return await _context.PostComments.AsNoTracking().Where(expression).OrderByDescending(c => c.DateCreated).ToListAsync();
        }

        public async Task<PostComment?> GetAsync(Guid id)
        {
            return await _context.PostComments.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Update(PostComment comment)
        {
            _context.PostComments.Update(comment);
        }
    }
}
