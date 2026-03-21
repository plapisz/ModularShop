using Microsoft.EntityFrameworkCore;
using ModularShop.Modules.Orders.Domain.Aggregates;
using ModularShop.Modules.Orders.Domain.Repositories;

namespace ModularShop.Modules.Orders.Infrastructure.Persistence.Write.Repositories;

internal sealed class OrderRepository(OrdersDbContext context) : IOrderRepository
{
    public async Task<Order?> GetAsync(Guid id, CancellationToken cancellationToken)
        => await context.Orders 
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        await context.Orders.AddAsync(order, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Order order, CancellationToken cancellationToken)
    {
        context.Orders.Update(order);
        await context.SaveChangesAsync(cancellationToken);
    }
}