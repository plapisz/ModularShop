using ModularShop.Modules.Orders.Domain.Exceptions;
using ModularShop.Modules.Orders.Domain.ValueObjects;

namespace ModularShop.Modules.Orders.Domain.Entities;

public sealed class ProductSnapshot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Money UnitPrice { get; private set; }
    public bool IsActive { get; private set; }

    public ProductSnapshot(Guid id, string name, Money unitPrice)
    {
        if (string.IsNullOrWhiteSpace(name))
        { 
            throw new InvalidProductNameException(); 
        }
        
        Id = id;
        Name = name;
        UnitPrice = unitPrice;
        IsActive = true;
    }

    public void Update(string name, Money unitPrice)
    {
        if (string.IsNullOrWhiteSpace(name))
        { 
            throw new InvalidProductNameException(); 
        }
        
        Name = name;
        UnitPrice = unitPrice;
    }
    
    public void Deactivate()
    {
        IsActive = false;
    }
}