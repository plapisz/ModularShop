using Microsoft.Extensions.Configuration;

namespace ModularShop.Shared.Infrastructure.Modules;

internal static class ModuleConfiguration
{
    public static bool IsEnabled(IConfiguration configuration, string moduleName)
    {
        var section = configuration.GetSection($"{moduleName}:module");
        if (!section.Exists())
        {
            return true;
        }

        var options = section.Get<ModuleOptions>();

        return options?.Enabled ?? true;
    }
}