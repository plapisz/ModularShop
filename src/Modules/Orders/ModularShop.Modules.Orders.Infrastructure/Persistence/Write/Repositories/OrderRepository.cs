using Microsoft.EntityFrameworkCore;
using ModularShop.Modules.Orders.Domain.Aggregates;
using ModularShop.Modules.Orders.Domain.Repositories;

namespace ModularShop.Modules.Orders.Infrastructure.Persistence.Write.Repositories;

internal sealed class OrderRepository(OrdersDbContext context) : IOrderRepository
{
    public async Task<Order?> GetAsync(Guid id) 
        => await context.Orders 
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(Order order)
    {
        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        context.Orders.Update(order);
        await context.SaveChangesAsync();
    }
}