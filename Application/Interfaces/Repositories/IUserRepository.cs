using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        void Update(User user);
        Task<User?> GetAsync(Guid id);
        Task<bool> CheckAsyncPhoneNumber(string phoneNumber);
        Task<User?> GetAsync(Expression<Func<User,bool>> expression);
    }
}
