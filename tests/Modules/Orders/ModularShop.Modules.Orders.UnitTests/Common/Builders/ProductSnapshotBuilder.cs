using ModularShop.Modules.Orders.Domain.Entities;
using ModularShop.Modules.Orders.Domain.ValueObjects;

namespace ModularShop.Modules.Orders.UnitTests.Common.Builders;

public sealed class ProductSnapshotBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _name = "Test Product";
    private Money _unitPrice = new(100m, "PLN");

    public ProductSnapshotBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public ProductSnapshotBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ProductSnapshotBuilder WithUnitPrice(Money unitPrice)
    {
        _unitPrice = unitPrice;
        return this;
    }

    public ProductSnapshot Build()
        => new(_id, _name, _unitPrice);
}