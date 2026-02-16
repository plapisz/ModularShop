using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ModularShop.Shared.Abstractions.Modules;

public interface IModule
{
    string Name { get; }
    string Path { get; }
    IReadOnlyCollection<string> Policies { get; }
    void Register(IServiceCollection services, IConfiguration configuration);
}