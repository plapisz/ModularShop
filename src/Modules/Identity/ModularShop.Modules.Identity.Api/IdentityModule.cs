using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularShop.Shared.Abstractions.Modules;

namespace ModularShop.Modules.Identity.Api;

internal sealed class IdentityModule : IModule
{
    internal const string BasePath = "identity";
    
    public string Name => "Identity";
    public string Path => BasePath;

    public void Register(IServiceCollection services, IConfiguration configuration)
    {
    }
}