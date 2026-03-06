using ModularShop.Modules.Orders.Domain.Repositories;
using ModularShop.Shared.Abstractions.Events;

namespace ModularShop.Modules.Orders.Application.Events.External.Handlers;

public sealed class ProductDeactivatedEventHandler(IProductSnapshotRepository productSnapshotRepository)
    : IEventHandler<ProductDeactivatedEvent>
{
    public async Task HandleAsync(ProductDeactivatedEvent @event)
    {
        var productSnapshot = await productSnapshotRepository.GetAsync(@event.Id);
        if (productSnapshot is null)
        {
            return;
        }
        
        productSnapshot.Deactivate();
        await productSnapshotRepository.UpdateAsync(productSnapshot);
    }
}