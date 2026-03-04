using ModularShop.Shared.Abstractions.Events;

namespace ModularShop.Shared.Infrastructure.Events;

internal sealed class EventPublisher(IModuleClient moduleClient) : IEventPublisher
{
    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent
        => await moduleClient.PublishAsync(@event);
}