namespace ModularShop.Shared.Infrastructure.Modules;

internal sealed record ModuleOptions
{
    public bool Enabled { get; init; } = true;
}