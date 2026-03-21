namespace ModularShop.Shared.Infrastructure.Events;

internal sealed class ModuleRegistry : IModuleRegistry
{
    private readonly List<ModuleBroadcastRegistration> _broadcastRegistrations = [];

    public IReadOnlyCollection<ModuleBroadcastRegistration> GetBroadcastRegistrations(string key)
        => _broadcastRegistrations.Where(x => x.Key == key).ToList();

    public void AddBroadcastAction(Type requestType, Func<object, CancellationToken, Task> action)
    {
        var registration = new ModuleBroadcastRegistration(requestType, action);
        _broadcastRegistrations.Add(registration);
    }
}