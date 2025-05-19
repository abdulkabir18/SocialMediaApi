using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IChatRepository
    {
        Task AddAsync(Chat chat);
        void Update(Chat chat);
        Task<Chat?> GetAsync(Guid id);
        Task<ICollection<Chat>> GetAllAsync(Guid mediaUserId);
    }
}
