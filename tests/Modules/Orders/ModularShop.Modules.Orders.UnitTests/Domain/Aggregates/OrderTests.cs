using ModularShop.Modules.Orders.Domain.Aggregates;
using ModularShop.Modules.Orders.Domain.Enums;
using ModularShop.Modules.Orders.Domain.Exceptions;
using ModularShop.Modules.Orders.Domain.ValueObjects;
using ModularShop.Modules.Orders.UnitTests.Common.Builders;
using Shouldly;

namespace ModularShop.Modules.Orders.UnitTests.Domain.Aggregates;

public sealed class OrderTests
{
    [Fact]
    public void GivenCreate_WhenValidData_ThenCreatesOrderWithCorrectValues()
    {
        // Arrange
        var id = Guid.NewGuid();
        var customerId = Guid.NewGuid();

        // Act
        var order = Order.Create(id, customerId);

        // Assert
        order.Id.ShouldBe(id);
        order.CustomerId.ShouldBe(customerId);
        order.Number.ShouldNotBeNull();
        order.Number.Value.ShouldStartWith("ORD-");
        order.Status.ShouldBe(OrderStatus.Draft);
        order.Items.ShouldBeEmpty();
    }
    
    [Fact]
    public void GivenAddItem_WhenOrderIsDraft_ThenAddsItem() 
    {
        //Arrange
        var order = new OrderBuilder().Build(); 
        var productId = Guid.NewGuid();
        var price = new Money(100m, "PLN");
        const string productName = "Test Product";
        const int quantity = 2;
        
        // Act
        order.AddItem(productId, productName, price, quantity);
        
        // Assert
        order.Items.ShouldHaveSingleItem();
        
        var item = order.Items.Single();
        item.ProductId.ShouldBe(productId);
        item.ProductName.ShouldBe(productName);
        item.UnitPrice.ShouldBe(price);
        item.Quantity.ShouldBe(quantity);
    }
    
    [Fact] 
    public void GivenAddItem_WhenProductAlreadyExists_ThenIncreasesQuantity() 
    { 
        // Arrange
        var productId = Guid.NewGuid(); 
        var order = new OrderBuilder() 
            .WithItem(productId: productId, quantity: 2) 
            .Build();
        
        // Act
        order.AddItem(productId, "Test Product", new Money(100m, "PLN"), 3);
        
        // Assert
        order.Items.ShouldHaveSingleItem();
        order.Items.Single().Quantity.ShouldBe(5);
    }
    
    [Fact] 
    public void GivenAddItem_WhenOrderIsConfirmed_ThenThrowsOrderIsNotDraftException() 
    {
        // Arrange
        var order = new OrderBuilder() 
            .WithItem() 
            .AsConfirmed() 
            .Build();
        
        // Act
        var exception = Record.Exception(() => order.AddItem(Guid.NewGuid(), "Test Product", new Money(100m, "PLN"), 1));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<OrderIsNotDraftException>(); 
    }

    [Fact]
    public void GivenAddItem_WhenOrderIsCancelled_ThenThrowsOrderIsNotDraftException()
    {
        // Arrange
        var order = new OrderBuilder()
            .WithItem()
            .AsCancelled()
            .Build();

        // Act
        var exception = Record.Exception(() => 
            order.AddItem(Guid.NewGuid(), "Test product", new Money(100m, "PLN"), 1));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<OrderIsNotDraftException>();
    }
    
    [Fact]
    public void GivenRemoveItem_WhenItemExists_ThenRemovesItem()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var order = new OrderBuilder()
            .WithItem(productId: productId)
            .Build();

        // Act
        order.RemoveItem(productId);

        // Assert
        order.Items.ShouldBeEmpty();
    }

    [Fact]
    public void GivenRemoveItem_WhenItemDoesNotExist_ThenOrderItemsAreUnchanged()
    {
        // Arrange
        var order = new OrderBuilder()
            .WithItem()
            .Build();

        // Act
        order.RemoveItem(Guid.NewGuid());

        // Assert
        order.Items.Count.ShouldBe(1);
    }

    [Fact]
    public void GivenRemoveItem_WhenOrderIsConfirmed_ThenThrowsOrderIsNotDraftException()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var order = new OrderBuilder()
            .WithItem(productId: productId)
            .AsConfirmed()
            .Build();

        // Act
        var exception = Record.Exception(() => order.RemoveItem(Guid.NewGuid()));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<OrderIsNotDraftException>();
    }

    [Fact]
    public void GivenRemoveItem_WhenOrderIsCancelled_ThenThrowsOrderIsNotDraftException()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var order = new OrderBuilder()
            .WithItem(productId: productId)
            .AsCancelled()
            .Build();

        // Act
        var exception = Record.Exception(() => order.RemoveItem(Guid.NewGuid()));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<OrderIsNotDraftException>();
    }
    
    [Fact]
    public void GivenConfirm_WhenOrderHasItems_ThenStatusIsConfirmed()
    {
        // Arrange
        var order = new OrderBuilder()
            .WithItem()
            .Build();

        // Act
        order.Confirm();

        // Assert
        order.Status.ShouldBe(OrderStatus.Confirmed);
    }

    [Fact]
    public void GivenConfirm_WhenOrderIsEmpty_ThenThrowsEmptyOrderException()
    {
        // Arrange
        var order = new OrderBuilder().Build();

        // Act
        var exception = Record.Exception(() => order.Confirm());
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyOrderException>();
    }

    [Fact]
    public void GivenConfirm_WhenOrderIsConfirmed_ThenThrowsOrderIsNotDraftException()
    {
        // Arrange
        var order = new OrderBuilder()
            .WithItem()
            .AsConfirmed()
            .Build();
        
        // Act
        var exception = Record.Exception(() => order.Confirm());
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<OrderIsNotDraftException>();
    }

    [Fact]
    public void GivenConfirm_WhenOrderIsCancelled_ThenThrowsOrderIsNotDraftException()
    {
        // Arrange
        var order = new OrderBuilder()
            .AsCancelled()
            .Build();
        
        // Act
        var exception = Record.Exception(() => order.Confirm());
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<OrderIsNotDraftException>();
    }
    
    [Fact]
    public void GivenCancel_WhenOrderIsDraft_ThenStatusIsCancelled()
    {
        // Arrange
        var order = new OrderBuilder().Build();

        // Act
        order.Cancel();

        // Assert
        order.Status.ShouldBe(OrderStatus.Cancelled);
    }

    [Fact]
    public void GivenCancel_WhenOrderIsConfirmed_ThenThrowsOrderAlreadyConfirmedException()
    {
        // Arrange
        var order = new OrderBuilder()
            .WithItem()
            .AsConfirmed()
            .Build();
        
        // Act
        var exception = Record.Exception(() => order.Cancel());
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<OrderAlreadyConfirmedException>();
    }

    [Fact]
    public void GivenCancel_WhenOrderIsAlreadyCancelled_ThenStatusRemainsCancelled()
    {
        // Arrange
        var order = new OrderBuilder()
            .AsCancelled()
            .Build();

        // Act
        order.Cancel();

        // Assert
        order.Status.ShouldBe(OrderStatus.Cancelled);
    }
    
    [Fact]
    public void GivenTotalPrice_WhenOrderHasItems_ThenReturnsSumOfAllItemPrices()
    {
        // Arrange
        var order = new OrderBuilder()
            .WithItem(price: new Money(100m, "PLN"), quantity: 2)
            .WithItem(price: new Money(50m, "PLN"), quantity: 3)
            .Build();

        // Act
        var result = order.TotalPrice;
        
        // Assert
        result.Amount.ShouldBe(350m);
        result.Currency.ShouldBe("PLN");
    }

    [Fact]
    public void GivenTotalPrice_WhenOrderIsEmpty_ThenThrowsEmptyMoneyCollectionException()
    {
        // Arrange
        var order = new OrderBuilder().Build();

        // Act
        var exception = Record.Exception(() => order.TotalPrice);
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyMoneyCollectionException>();
    }
}