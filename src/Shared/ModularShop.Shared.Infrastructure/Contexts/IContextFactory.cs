using ModularShop.Shared.Abstractions.Contexts;

namespace ModularShop.Shared.Infrastructure.Contexts;

internal interface IContextFactory
{
    IContext Create();
}