using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistense.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistense.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ProjectContext _context;
        public UserRepository(ProjectContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            await _context.Set<User>().AddAsync(user);
        }

        public async Task<bool> CheckAsyncPhoneNumber(string phoneNumber)
        {
            return await _context.Set<User>().AsNoTracking().AnyAsync(u => u.PhoneNumber == phoneNumber);
        }

        public async Task<User?> GetAsync(Guid id)
        {
            return await _context.Set<User>().AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetAsync(Expression<Func<User, bool>> expression)
        {
            return await _context.Set<User>().AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public void Update(User user)
        {
             _context.Set<User>().Update(user);
        }
    }
}
