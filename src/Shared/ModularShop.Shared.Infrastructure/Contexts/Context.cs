using Microsoft.AspNetCore.Http;
using ModularShop.Shared.Abstractions.Contexts;

namespace ModularShop.Shared.Infrastructure.Contexts;

internal sealed class Context : IContext
{
    public string RequestId { get; } = $"{Guid.NewGuid():N}";
    public string TraceId { get; }
    public IIdentityContext Identity { get; }

    private Context(string traceId, IIdentityContext identity)
    {
        TraceId = traceId;
        Identity = identity;
    }
    
    internal Context(HttpContext context) : this(context.TraceIdentifier, new IdentityContext(context.User))
    {
    }
    
    internal static IContext CreateEmpty()
        => new Context(string.Empty, IdentityContext.CreateEmpty());
}