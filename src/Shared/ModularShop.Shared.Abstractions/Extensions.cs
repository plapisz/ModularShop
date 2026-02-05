using Microsoft.Extensions.DependencyInjection;

namespace ModularShop.Shared.Abstractions;

public static class Extensions
{
    public static IServiceCollection RegisterOptions<TOptions>(
        this IServiceCollection services,
        string sectionName)
        where TOptions : class
    {
        services.AddOptions<TOptions>()
            .BindConfiguration(sectionName);

        return services;
    }
}