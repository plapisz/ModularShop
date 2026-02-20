using Microsoft.EntityFrameworkCore;
using ModularShop.Modules.Identity.Core.Entities;

namespace ModularShop.Modules.Identity.Core.Infrastructure.Persistence;

internal sealed class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("identity");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}