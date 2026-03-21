namespace ModularShop.Shared.Infrastructure.Events;

internal interface IModuleRegistry
{
    IReadOnlyCollection<ModuleBroadcastRegistration> GetBroadcastRegistrations(string key); 
    void AddBroadcastAction(Type requestType, Func<object, CancellationToken, Task> action);
}