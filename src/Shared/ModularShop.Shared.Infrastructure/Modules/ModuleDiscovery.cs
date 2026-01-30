using System.Reflection;
using ModularShop.Shared.Abstractions.Modules;

namespace ModularShop.Shared.Infrastructure.Modules;

internal static class ModuleDiscovery
{
    public static IReadOnlyCollection<IModule> Discover() 
        => GetAllAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t =>
                !t.IsAbstract &&
                typeof(IModule).IsAssignableFrom(t))
            .Select(Activator.CreateInstance)
            .Cast<IModule>()
            .ToList();
    
    private static List<Assembly> GetAllAssemblies()
    {
        var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic)
            .ToList();

        var loadedLocations = loadedAssemblies
            .Select(a => a.Location)
            .ToHashSet(StringComparer.InvariantCultureIgnoreCase);

        var assembliesFromDisk = Directory
            .GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Where(path => !loadedLocations.Contains(path))
            .Select(path => AppDomain.CurrentDomain.Load(
                AssemblyName.GetAssemblyName(path)));

        return loadedAssemblies.Concat(assembliesFromDisk).ToList();
    }
}