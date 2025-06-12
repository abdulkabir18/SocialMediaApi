using System.Linq.Expressions;
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
            await _context.Posts.AddAsync(post);
        }

        public async Task<bool> CheckAsync(Guid id)
        {
            return await _context.Posts.AsNoTracking().AnyAsync(p => p.Id == id);
        }

        public void Delete(Post post)
        {
            _context.Posts.Remove(post);
        }

        public async Task<ICollection<Post>> GetAllAsync(Guid posterId)
        {
            return await _context.Posts.AsNoTracking().Where(p => p.PosterId  == posterId).OrderByDescending(p => p.DateCreated).ToListAsync();
        }

        public async Task<ICollection<Post>> GetAllAsync()
        {
            return await _context.Posts.Take(10).AsNoTracking().OrderByDescending(p => p.DateCreated).ToListAsync();
        }

        public async Task<Post?> GetAsync(Guid id)
        {
            return await _context.Posts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Post?> GetAsync(Expression<Func<Post, bool>> expression)
        {
            return await _context.Posts.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public void Update(Post post)
        {
            _context.Posts.Update(post);
        }
    }
}
