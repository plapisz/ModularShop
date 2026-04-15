using ModularShop.Modules.Orders.Domain.Entities;
using ModularShop.Modules.Orders.Domain.Exceptions;
using ModularShop.Modules.Orders.Domain.ValueObjects;
using ModularShop.Modules.Orders.UnitTests.Common.Builders;
using Shouldly;

namespace ModularShop.Modules.Orders.UnitTests.Domain.Entities;

public sealed class OrderItemTests
{
    [Fact]
    public void GivenConstructor_WhenValidData_ThenCreatesOrderItemWithCorrectValues()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var unitPrice = new Money(100m, "PLN");
        const string productName = "Test Product";
        const int quantity = 2;
        
        // Act
        var item = new OrderItem(productId, productName, unitPrice, quantity);
        
        // Assert
        item.ProductId.ShouldBe(productId);
        item.ProductName.ShouldBe(productName);
        item.UnitPrice.ShouldBe(unitPrice);
        item.Quantity.ShouldBe(quantity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void GivenConstructor_WhenNonPositiveQuantity_ThenThrowsInvalidQuantityException(int quantity)
    {
        // Arrange
        var unitPrice = new Money(100m, "PLN");
        var productId = Guid.NewGuid();
        const string productName = "Test Product";

        // Act
        var exception = Record.Exception(() => new OrderItem(productId, productName, unitPrice, quantity));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidQuantityException>();
    }
    
    [Theory]
    [InlineData(100, 1, 100)]
    [InlineData(100, 3, 300)]
    [InlineData(99.99, 2, 199.98)]
    public void GivenTotalPrice_WhenCalled_ThenReturnsUnitPriceMultipliedByQuantity(decimal unitPrice, int quantity, decimal expectedTotal)
    {
        // Arrange
        var item = new OrderItemBuilder()
            .WithUnitPrice(new Money(unitPrice, "PLN"))
            .WithQuantity(quantity)
            .Build();

        // Act
        var totalPrice = item.TotalPrice;
        
        // Assert
        totalPrice.Amount.ShouldBe(expectedTotal);
    }

    [Fact]
    public void GivenTotalPrice_WhenCalled_ThenRetainsCurrency()
    {
        // Arrange
        const string currency = "PLN";
        var item = new OrderItemBuilder()
            .WithUnitPrice(new Money(100m, currency))
            .WithQuantity(2)
            .Build();

        // Act
        var totalPrice = item.TotalPrice;
        
        // Assert
        totalPrice.Currency.ShouldBe(currency);
    }
    
    [Fact]
    public void GivenChangeQuantity_WhenValidQuantity_ThenUpdatesQuantity()
    {
        // Arrange
        var item = new OrderItemBuilder()
            .WithQuantity(1)
            .Build();

        // Act
        item.ChangeQuantity(5);

        // Assert
        item.Quantity.ShouldBe(5);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void GivenChangeQuantity_WhenNonPositiveQuantity_ThenThrowsInvalidQuantityException(int quantity)
    {
        // Arrange
        var item = new OrderItemBuilder().Build();

        // Act
        var exception = Record.Exception(() => item.ChangeQuantity(quantity));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidQuantityException>();
    }
}