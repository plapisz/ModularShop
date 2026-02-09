using Microsoft.EntityFrameworkCore;
using ModularShop.Modules.Catalog.Core.Entities;

namespace ModularShop.Modules.Catalog.Core.Infrastructure.Persistence;

internal sealed class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("catalog");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}