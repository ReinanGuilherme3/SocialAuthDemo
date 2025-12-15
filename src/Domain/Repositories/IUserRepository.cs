using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetByExternalProviderAsync(string provider, string externalId);
}
