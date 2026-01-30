using Microsoft.Extensions.DependencyInjection;

namespace ModularShop.Shared.Abstractions.Modules;

public interface IModule
{
    string Name { get; }
    string Path { get; }
    void Register(IServiceCollection services);
}