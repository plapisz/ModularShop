using ModularShop.Shared.Abstractions.Events;

namespace ModularShop.Shared.Infrastructure.Events;

internal sealed class ModuleClient(IModuleRegistry moduleRegistry, IModuleSerializer moduleSerializer) 
    : IModuleClient
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : class, IEvent
    {
        var key = @event.GetType().Name;
        var registrations = moduleRegistry
            .GetBroadcastRegistrations(key)
            .Where(r => r.ReceiverType != @event.GetType());

        var tasks = new List<Task>();
            
        foreach (var registration in registrations)
        {
            var action = registration.Action;
            var receiverMessage = TranslateType(@event, registration.ReceiverType);
            tasks.Add(action(receiverMessage, cancellationToken));
        }

        await Task.WhenAll(tasks);
    }

    private object TranslateType(object value, Type type)
        => moduleSerializer.Deserialize(moduleSerializer.Serialize(value), type);
}