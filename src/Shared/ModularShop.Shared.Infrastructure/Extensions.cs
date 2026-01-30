using Microsoft.Extensions.DependencyInjection;
using ModularShop.Shared.Infrastructure.Api;
using ModularShop.Shared.Infrastructure.Modules;

namespace ModularShop.Shared.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var modules = ModuleDiscovery.Discover();
        foreach (var module in modules)
        {
            module.Register(services);
        }

        services 
            .AddControllers()
            .AddModules(modules);

        return services;
    }
}