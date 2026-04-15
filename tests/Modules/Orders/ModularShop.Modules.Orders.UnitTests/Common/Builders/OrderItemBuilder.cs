using ModularShop.Modules.Orders.Domain.Entities;
using ModularShop.Modules.Orders.Domain.ValueObjects;

namespace ModularShop.Modules.Orders.UnitTests.Common.Builders;

public sealed class OrderItemBuilder
{
    private Guid _productId = Guid.NewGuid();
    private string _productName = "Test Product";
    private Money _unitPrice = new(100m, "PLN");
    private int _quantity = 1;

    public OrderItemBuilder WithProductId(Guid productId)
    {
        _productId = productId;
        return this;
    }

    public OrderItemBuilder WithProductName(string productName)
    {
        _productName = productName;
        return this;
    }

    public OrderItemBuilder WithUnitPrice(Money unitPrice)
    {
        _unitPrice = unitPrice;
        return this;
    }

    public OrderItemBuilder WithQuantity(int quantity)
    {
        _quantity = quantity;
        return this;
    }

    public OrderItem Build()
        => new(_productId, _productName, _unitPrice, _quantity);
}