using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repositories;

internal class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(SocialAuthDemoDbContext context) : base(context) { }

    public async Task<User?> GetByExternalProviderAsync(string provider, string externalId)
    {
        return await _dbSet.FirstOrDefaultAsync(u =>
            u.ExternalProvider == provider && u.ExternalId == externalId);
    }
}
