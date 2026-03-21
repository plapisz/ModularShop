using Microsoft.EntityFrameworkCore;
using ModularShop.Modules.Identity.Core.Entities;
using ModularShop.Modules.Identity.Core.Repositories;

namespace ModularShop.Modules.Identity.Core.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository(IdentityDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await context.Users.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        => await context.Users.SingleOrDefaultAsync(x => x.Email == email, cancellationToken);

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}