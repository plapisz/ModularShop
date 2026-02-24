using ModularShop.Modules.Orders.Domain.Entities;
using ModularShop.Modules.Orders.Domain.Enums;
using ModularShop.Modules.Orders.Domain.Exceptions;
using ModularShop.Modules.Orders.Domain.ValueObjects;

namespace ModularShop.Modules.Orders.Domain.Aggregates;

public sealed class Order
{
    private readonly List<OrderItem> _items = [];

    public Guid Id { get; }
    public OrderNumber Number { get; }
    public Guid CustomerId { get; }
    public OrderStatus Status { get; private set; }
    
    public Money TotalPrice => Money.Sum(_items.Select(i => i.TotalPrice).ToList());
    public IReadOnlyCollection<OrderItem> Items => _items;

    private Order(Guid id, Guid customerId)
    {
        Id = id;
        Number = OrderNumber.New();
        CustomerId = customerId;
        Status = OrderStatus.Draft;
    }

    public static Order Create(Guid id, Guid customerId)
        => new(id, customerId);

    public void AddItem(Guid productId, string productName, Money price, int quantity)
    {
        EnsureDraft();

        var existing = _items.FirstOrDefault(x => x.ProductId == productId);
        if (existing is not null)
        {
            existing.ChangeQuantity(existing.Quantity + quantity);
            return;
        }

        _items.Add(new OrderItem(productId, productName, price, quantity));
    }

    public void RemoveItem(Guid productId)
    {
        EnsureDraft();
        _items.RemoveAll(x => x.ProductId == productId);
    }

    public void Confirm()
    {
        EnsureDraft();
        if (_items.Count == 0)
        {
            throw new EmptyOrderException(Number.ToString());
        }

        Status = OrderStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Confirmed)
        {
            throw new OrderAlreadyConfirmedException(Number.ToString());
        }
        
        Status = OrderStatus.Cancelled;
    }

    private void EnsureDraft()
    {
        if (Status != OrderStatus.Draft)
        {
            throw new OrderIsNotDraftException(Number.ToString());
        }
    }
}