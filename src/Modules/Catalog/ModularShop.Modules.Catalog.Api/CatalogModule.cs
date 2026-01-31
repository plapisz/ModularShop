using Microsoft.Extensions.DependencyInjection;
using ModularShop.Modules.Catalog.Core;
using ModularShop.Shared.Abstractions.Modules;

namespace ModularShop.Modules.Catalog.Api;

internal sealed class CatalogModule : IModule
{
    public string Name => "Catalog";
    public string Path => "catalog";
    
    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }
}