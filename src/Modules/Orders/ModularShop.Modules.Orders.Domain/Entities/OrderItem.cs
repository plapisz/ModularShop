using ModularShop.Modules.Orders.Domain.Exceptions;
using ModularShop.Modules.Orders.Domain.ValueObjects;

namespace ModularShop.Modules.Orders.Domain.Entities;

public sealed class OrderItem
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public Money UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    public Money TotalPrice => UnitPrice.Multiply(Quantity);

    public OrderItem(Guid productId, string productName, Money unitPrice, int quantity)
    {
        if (quantity <= 0)
        {
            throw new InvalidQuantityException(quantity);
        }

        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public void ChangeQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new InvalidQuantityException(quantity);
        }
        
        Quantity = quantity;
    }
}