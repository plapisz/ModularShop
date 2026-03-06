using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ModularShop.Shared.Abstractions.Events;

namespace ModularShop.Shared.Infrastructure.Events;

internal static class Extensions
{
    extension(IServiceCollection services)
    {
        internal IServiceCollection AddEventHandlers(IReadOnlyCollection<Assembly> assemblies)
        {
            services.Scan(s => s.FromAssemblies(assemblies) 
                .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>))) 
                .AsImplementedInterfaces() 
                .WithScopedLifetime());
            
            return services;
        }

        internal IServiceCollection AddModuleRequests(IReadOnlyCollection<Assembly> assemblies)
        {
            services.AddModuleRegistry(assemblies);
            services.AddSingleton<IEventPublisher, EventPublisher>();
            services.AddSingleton<IModuleClient, ModuleClient>();
            services.AddSingleton<IModuleSerializer, JsonModuleSerializer>();

            return services;
        }

        private void AddModuleRegistry(IEnumerable<Assembly> assemblies)
        {
            services.AddSingleton<IModuleRegistry>(sp =>
            {
                var registry = new ModuleRegistry();

                var types = assemblies.SelectMany(x => x.GetTypes());

                var handlerTypes = types
                    .Where(t => t is { IsClass: true, IsAbstract: false })
                    .SelectMany(t => t.GetInterfaces()
                        .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>))
                        .Select(i => new
                        {
                            HandlerInterface = i,
                            EventType = i.GetGenericArguments()[0]
                        }));

                foreach (var handler in handlerTypes)
                {
                    registry.AddBroadcastAction(handler.EventType, async @event =>
                    {
                        using var scope = sp.CreateScope();
                        var instance = scope.ServiceProvider.GetRequiredService(handler.HandlerInterface);
                        await (Task)handler.HandlerInterface
                            .GetMethod(nameof(IEventHandler<>.HandleAsync))!
                            .Invoke(instance, [@event])!;
                    });
                }

                return registry;
            });
        }
    }
}