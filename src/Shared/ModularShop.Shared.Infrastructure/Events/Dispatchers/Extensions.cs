using Microsoft.Extensions.DependencyInjection;

namespace ModularShop.Shared.Infrastructure.Events.Dispatchers;

public static class Extensions
{
    public static IServiceCollection AddAsyncEventDispatcher(this IServiceCollection services)
    {
        services.AddSingleton<IEventChannel, EventChannel>();
        services.AddSingleton<IAsyncEventDispatcher, AsyncEventDispatcher>();
        services.AddHostedService<BackgroundDispatcher>();
        
        return services;
    }
}