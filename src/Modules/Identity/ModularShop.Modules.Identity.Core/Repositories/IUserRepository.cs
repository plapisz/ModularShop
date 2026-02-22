using ModularShop.Modules.Identity.Core.Entities;

namespace ModularShop.Modules.Identity.Core.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
}