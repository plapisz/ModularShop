namespace ModularShop.Modules.Identity.Core.Time;

internal sealed class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}