using ModularShop.Modules.Identity.Core.Entities;

namespace ModularShop.Modules.Identity.Core.Repositories;

public sealed class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = [];

    public Task<User?> GetByEmailAsync(string email)
    {
        var user = _users.FirstOrDefault(u => u.Email == email);
        return Task.FromResult(user);
    }

    public Task AddAsync(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }
}