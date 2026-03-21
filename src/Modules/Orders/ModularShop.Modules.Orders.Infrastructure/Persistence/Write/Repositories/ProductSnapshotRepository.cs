using Microsoft.EntityFrameworkCore;
using ModularShop.Modules.Orders.Domain.Entities;
using ModularShop.Modules.Orders.Domain.Repositories;

namespace ModularShop.Modules.Orders.Infrastructure.Persistence.Write.Repositories;

internal sealed class ProductSnapshotRepository(OrdersDbContext context) : IProductSnapshotRepository
{
    public Task<ProductSnapshot?> GetAsync(Guid id, CancellationToken cancellationToken)
        => context.ProductSnapshots.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task AddAsync(ProductSnapshot snapshot, CancellationToken cancellationToken)
    {
        await context.ProductSnapshots.AddAsync(snapshot, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ProductSnapshot snapshot, CancellationToken cancellationToken)
    {
        context.ProductSnapshots.Update(snapshot);
        await context.SaveChangesAsync(cancellationToken);
    }
}
