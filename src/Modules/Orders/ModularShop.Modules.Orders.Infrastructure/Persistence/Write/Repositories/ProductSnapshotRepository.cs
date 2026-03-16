using Microsoft.EntityFrameworkCore;
using ModularShop.Modules.Orders.Domain.Entities;
using ModularShop.Modules.Orders.Domain.Repositories;

namespace ModularShop.Modules.Orders.Infrastructure.Persistence.Write.Repositories;

internal sealed class ProductSnapshotRepository(OrdersDbContext context) : IProductSnapshotRepository
{
    public Task<ProductSnapshot?> GetAsync(Guid id)
        => context.ProductSnapshots.FirstOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(ProductSnapshot snapshot)
    {
        await context.ProductSnapshots.AddAsync(snapshot);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductSnapshot snapshot)
    {
        context.ProductSnapshots.Update(snapshot);
        await context.SaveChangesAsync();
    }
}
