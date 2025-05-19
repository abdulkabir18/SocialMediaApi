using Application.Interfaces.UnitOfWork;
using Infrastructure.Persistense.Context;

namespace Infrastructure.Persistense.UnitOfWork
{
    public class UnitOfWork(ProjectContext context) : IUnitOfWork
    {
        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
