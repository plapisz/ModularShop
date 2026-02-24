using ModularShop.Modules.Orders.Domain.Exceptions;

namespace ModularShop.Modules.Orders.Domain.ValueObjects;

public sealed record OrderNumber
{
    public string Value { get; }

    private OrderNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOrderNumberException();
        }

        Value = value;
    }

    public static OrderNumber New()
        => new($"ORD-{Guid.NewGuid():N[..6].ToUpperInvariant()}");

    public static OrderNumber From(string value)
        => new(value);

    public override string ToString() => Value;
}