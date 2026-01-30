using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using ModularShop.Shared.Abstractions.Modules;

namespace ModularShop.Shared.Infrastructure.Api;

internal static class Extensions
{
    public static IMvcBuilder AddModules(this IMvcBuilder builder, IEnumerable<IModule> modules)
    {
        foreach (var module in modules)
        {
            builder.PartManager.ApplicationParts.Add(
                new AssemblyPart(module.GetType().Assembly));
        }
        
        builder.ConfigureApplicationPartManager(manager =>
        {
            manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
        });

        return builder;
    }
}