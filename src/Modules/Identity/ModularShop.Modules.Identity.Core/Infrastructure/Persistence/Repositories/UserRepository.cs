using Microsoft.EntityFrameworkCore;
using ModularShop.Modules.Identity.Core.Entities;
using ModularShop.Modules.Identity.Core.Repositories;

namespace ModularShop.Modules.Identity.Core.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository(IdentityDbContext context) : IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email)
        => await context.Users.SingleOrDefaultAsync(x => x.Email == email);

    public async Task AddAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }
}