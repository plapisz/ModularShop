using ModularShop.Modules.Orders.Domain.Entities;
using ModularShop.Modules.Orders.Domain.Repositories;
using ModularShop.Modules.Orders.Domain.ValueObjects;
using ModularShop.Shared.Abstractions.Events;

namespace ModularShop.Modules.Orders.Application.Events.External.Handlers;

public sealed class ProductCreatedEventHandler(IProductSnapshotRepository productSnapshotRepository)
    : IEventHandler<ProductCreatedEvent>
{
    public async Task HandleAsync(ProductCreatedEvent @event)
    {
        var productSnapshot = new ProductSnapshot(@event.Id, @event.Name, new Money(@event.Price, "PLN"));
        await productSnapshotRepository.AddAsync(productSnapshot);
    }
}