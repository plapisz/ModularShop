using Microsoft.AspNetCore.Http;
using ModularShop.Shared.Abstractions.Contexts;

namespace ModularShop.Shared.Infrastructure.Contexts;

internal sealed class ContextFactory(IHttpContextAccessor httpContextAccessor) : IContextFactory
{
    public IContext Create()
    {
        var httpContext = httpContextAccessor.HttpContext;

        return httpContext is null ? Context.CreateEmpty() : new Context(httpContext);;
    }
}