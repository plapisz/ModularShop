using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularShop.Modules.Catalog.Core;
using ModularShop.Modules.Catalog.Core.Options;
using ModularShop.Shared.Abstractions;
using ModularShop.Shared.Abstractions.Modules;

namespace ModularShop.Modules.Catalog.Api;

internal sealed class CatalogModule : IModule
{
    public string Name => "Catalog";
    public string Path => "catalog";
    public IReadOnlyCollection<string> Policies => [Security.Policies.Catalog.Read, Security.Policies.Catalog.Write];
    
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterOptions<CatalogOptions>("catalog");
        services.AddCore(configuration);
    }
}