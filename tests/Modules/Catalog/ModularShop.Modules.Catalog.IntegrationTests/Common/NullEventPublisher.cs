using ModularShop.Shared.Abstractions.Events;

namespace ModularShop.Modules.Catalog.IntegrationTests.Common;

internal sealed class NullEventPublisher : IEventPublisher
{
    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : class, IEvent 
        => Task.CompletedTask;
}