using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistense.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistense.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ProjectContext _context;
        public PostRepository(ProjectContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Post post)
        {
            await _context.Set<Post>().AddAsync(post);
        }

        public async Task<bool> CheckAsync(Guid id)
        {
            return await _context.Set<Post>().AsNoTracking().AnyAsync(p => p.Id == id);
        }

        public void Delete(Post post)
        {
            _context.Set<Post>().Remove(post);
        }

        public async Task<ICollection<Post>> GetAllAsync(Guid posterId)
        {
            return await _context.Set<Post>().AsNoTracking().Where(p => p.PosterId  == posterId).OrderBy(p => p.DateCreated).ToListAsync();
        }

        public async Task<ICollection<Post>> GetAllAsync()
        {
            return await _context.Set<Post>().AsNoTracking().OrderBy(p => p.DateCreated).ToListAsync();
        }

        public async Task<Post?> GetAsync(Guid id)
        {
            return await _context.Set<Post>().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Post?> GetAsync(Expression<Func<Post, bool>> expression)
        {
            return await _context.Set<Post>().AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public void Update(Post post)
        {
            _context.Set<Post>().Update(post);
        }
    }
}
