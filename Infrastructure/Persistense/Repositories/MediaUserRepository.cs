using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistense.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistense.Repositories
{
    public class MediaUserRepository : IMediaUserRepository
    {
        private readonly ProjectContext _context;
        public MediaUserRepository(ProjectContext projectContext)
        {
            _context = projectContext;   
        }
        public async Task AddAsync(MediaUser mediaUser)
        {
             await _context.Set<MediaUser>().AddAsync(mediaUser);
        }

        public async Task<bool> CheckAsync(Expression<Func<MediaUser, bool>> expression)
        {
            return await _context.Set<MediaUser>().AsNoTracking().AnyAsync(expression);
        }

        public async Task<ICollection<MediaUser>> GetAllAsync()
        {
            return await _context.Set<MediaUser>().AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<MediaUser>> GetAllAsync(Expression<Func<MediaUser, bool>> expression)
        {
            return await _context.Set<MediaUser>().AsNoTracking().Where(expression).ToListAsync();  
        }

        public async Task<MediaUser?> GetAsync(Guid id)
        {
            return await _context.Set<MediaUser>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<MediaUser?> GetAsync(Expression<Func<MediaUser, bool>> expression)
        {
            return await _context.Set<MediaUser>().AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public void Update(MediaUser mediaUser)
        {
            _context.Set<MediaUser>().Update(mediaUser);
        }
    }
}
