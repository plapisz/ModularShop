using ModularShop.Modules.Orders.Domain.Exceptions;

namespace ModularShop.Modules.Orders.Domain.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
        {
            throw new InvalidAmountException(amount);
        }

        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new InvalidCurrencyException();
        }

        Amount = amount;
        Currency = currency;
    }

    public static Money Zero(string currency)
        => new(0m, currency);

    public Money Multiply(int factor)
    {
        if (factor <= 0)
        {
            throw new InvalidMultiplierException(factor);
        }

        return new Money(Amount * factor, Currency);
    }

    public static Money Sum(IReadOnlyCollection<Money> values)
    {
        if (values is null || values.Count == 0)
        {
            throw new EmptyMoneyCollectionException();
        }

        var currency = values.First().Currency;
        if (values.Any(v => v.Currency != currency))
        {
            throw new InvalidCurrencyException();
        }

        return new Money(values.Sum(v => v.Amount), currency);
    }

    public override string ToString()
        => $"{Amount:0.00} {Currency}";
}