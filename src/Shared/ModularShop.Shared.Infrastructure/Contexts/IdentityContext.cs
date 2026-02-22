using System.Security.Claims;
using ModularShop.Shared.Abstractions.Contexts;

namespace ModularShop.Shared.Infrastructure.Contexts;

internal sealed class IdentityContext : IIdentityContext
{
    public bool IsAuthenticated { get; }
    public Guid Id { get; }
    public string Role { get; }
    public IReadOnlyDictionary<string, IReadOnlyCollection<string>> Claims { get; }
    
    private IdentityContext()
    {
        IsAuthenticated = false;
        Id = Guid.Empty;
        Role = string.Empty;
        Claims = new Dictionary<string, IReadOnlyCollection<string>>();
    }

    internal IdentityContext(ClaimsPrincipal principal)
    {
        IsAuthenticated = principal.Identity?.IsAuthenticated is true;
        Id = IsAuthenticated ? Guid.Parse(principal.Identity!.Name!) : Guid.Empty;
        Role = principal.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value ?? string.Empty;
        Claims = principal.Claims
            .GroupBy(x => x.Type)
            .ToDictionary(
                x => x.Key, 
                IReadOnlyCollection<string>(x) => x.Select(c => c.Value).ToList()
            );
    }
    
    internal static IIdentityContext CreateEmpty() 
        => new IdentityContext();
}