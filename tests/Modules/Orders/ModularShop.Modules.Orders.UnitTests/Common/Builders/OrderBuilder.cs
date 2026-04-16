using ModularShop.Modules.Orders.Domain.Aggregates;
using ModularShop.Modules.Orders.Domain.ValueObjects;

namespace ModularShop.Modules.Orders.UnitTests.Common.Builders;

public sealed class OrderBuilder
{
    private Guid _id = Guid.NewGuid();
    private Guid _customerId = Guid.NewGuid();
    private readonly List<(Guid ProductId, string ProductName, Money Price, int Quantity)> _items = [];
    private bool _confirmed;
    private bool _cancelled;

    public OrderBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public OrderBuilder WithCustomerId(Guid customerId)
    {
        _customerId = customerId;
        return this;
    }

    public OrderBuilder WithItem(
        Guid? productId = null,
        string productName = "Test Product",
        Money? price = null,
        int quantity = 1)
    {
        _items.Add((productId ?? Guid.NewGuid(), productName, price ?? new Money(100m, "PLN"), quantity));
        return this;
    }

    public OrderBuilder AsConfirmed()
    {
        _confirmed = true;
        return this;
    }

    public OrderBuilder AsCancelled()
    {
        _cancelled = true;
        return this;
    }

    public Order Build()
    {
        var order = Order.Create(_id, _customerId);

        foreach (var (productId, productName, price, quantity) in _items)
        {
            order.AddItem(productId, productName, price, quantity);
        }

        if (_confirmed)
        {
            order.Confirm();
        }

        if (_cancelled)
        {
            order.Cancel();
        }

        return order;
    }
}