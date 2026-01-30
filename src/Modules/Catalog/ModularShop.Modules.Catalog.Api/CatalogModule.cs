using Microsoft.Extensions.DependencyInjection;
using ModularShop.Shared.Abstractions.Modules;

namespace ModularShop.Modules.Catalog.Api;

internal sealed class CatalogModule : IModule
{
    internal const string BasePath = "catalog";
    
    public string Name => "Catalog";
    public string Path => BasePath;
    
    public void Register(IServiceCollection services)
    {
    }
}