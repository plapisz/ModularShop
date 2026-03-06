using ModularShop.Modules.Orders.Domain.Repositories;
using ModularShop.Modules.Orders.Domain.ValueObjects;
using ModularShop.Shared.Abstractions.Events;

namespace ModularShop.Modules.Orders.Application.Events.External.Handlers;

public sealed class ProductUpdatedEventHandler(IProductSnapshotRepository productSnapshotRepository) 
    : IEventHandler<ProductUpdatedEvent>
{
    public async Task HandleAsync(ProductUpdatedEvent @event)
    {
        var productSnapshot = await productSnapshotRepository.GetAsync(@event.Id);
        if (productSnapshot is null)
        {
            return;
        }
        
        productSnapshot.Update(@event.Name, new Money(@event.Price, "PLN"));
        await productSnapshotRepository.UpdateAsync(productSnapshot);
    }
}