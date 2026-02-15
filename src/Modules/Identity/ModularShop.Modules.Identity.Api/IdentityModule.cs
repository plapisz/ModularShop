using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularShop.Modules.Identity.Core;
using ModularShop.Shared.Abstractions.Modules;

namespace ModularShop.Modules.Identity.Api;

internal sealed class IdentityModule : IModule
{
    public string Name => "Identity";
    public string Path => "identity";

    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCore();
    }
}